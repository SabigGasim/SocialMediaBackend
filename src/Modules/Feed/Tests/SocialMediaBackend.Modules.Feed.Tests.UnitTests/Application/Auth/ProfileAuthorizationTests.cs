using Autofac;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Shouldly;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authorization;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Follows;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Feed.Infrastructure.Security;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common;
using SocialMediaBackend.Modules.Feed.Tests.Core.Common.Users;

namespace SocialMediaBackend.Modules.Feed.Tests.UnitTests.Application.Auth;


public class ProfileAuthorizationHandlerBaseTests(App app) : AppTestBase(app)
{
    [Fact]
    public async Task AuthorizeAsync_ShouldReturnTrue_WhenUserIsOwner()
    {
        // Arrange
        await using var scope = FeedCompositionRoot.BeginLifetimeScope();
        var context = scope.Resolve<FakeDbContext>();
        var handler = scope.Resolve<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>();

        var user = AuthorFactory.Create();
        var resource = FakeUserResource.Create(user);
        context.Add(user);
        context.Add(resource);

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var options = new AuthOptions(IsAdmin: false);

        // Act
        var result = await handler.AuthorizeAsync(user.Id, resource.Id, TestContext.Current.CancellationToken);

        // Assert
        result.IsAuthorized.ShouldBeTrue();
    }

    [Fact]
    public async Task AuthorizeAsync_ShouldReturnFalse_WhenProfileIsPrivate_AndNotAdmin()
    {
        // Arrange
        await using var scope = FeedCompositionRoot.BeginLifetimeScope();
        var context = scope.Resolve<FakeDbContext>();
        var handler = scope.Resolve<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>();

        var owner = AuthorFactory.Create(isPublic: false);
        var resource = FakeUserResource.Create(owner);
        context.Add(owner);
        context.Add(resource);

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var differentAuthorId = AuthorId.New();
        var options = new AuthOptions(IsAdmin: false);

        // Act
        var result = await handler.AuthorizeAsync(differentAuthorId, resource.Id, TestContext.Current.CancellationToken);

        // Assert
        result.IsAuthorized.ShouldBeFalse();
    }

    [Fact]
    public async Task IsAdminOrResourceOwnerAsync_ShouldReturnTrue_WhenUserIsAdmin()
    {
        // Arrange
        await using var scope = FeedCompositionRoot.BeginLifetimeScope();
        await using var context = scope.Resolve<FakeDbContext>();
        var factory = scope.Resolve<IDbConnectionFactory>();

        var userId = AuthorId.New();
        var resourceId = FakeUserResourceId.New();

        var permissionManager = Substitute.For<IPermissionManager>();
        permissionManager
            .UserIsInRole(userId.Value, (int)Roles.AdminAuthor, TestContext.Current.CancellationToken)
            .Returns(true);

        var handler = new FakeProfileAuthorizationHandler(context, permissionManager);

        // Act
        var result = await handler.IsAdminOrResourceOwnerAsync(userId, resourceId, TestContext.Current.CancellationToken);

        // Assert
        result.IsAuthorized.ShouldBeTrue();
    }

    [Fact]
    public async Task IsAdminOrResourceOwnerAsync_ShouldReturnTrue_WhenUserIsOwner()
    {
        // Arrange
        await using var scope = FeedCompositionRoot.BeginLifetimeScope();
        var context = scope.Resolve<FakeDbContext>();
        var handler = scope.Resolve<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>();

        var user = AuthorFactory.Create(isPublic: false);
        var resource = FakeUserResource.Create(user);

        context.Add(user);
        context.Add(resource);

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var options = new AuthOptions(IsAdmin: false);

        // Act
        var result = await handler.IsAdminOrResourceOwnerAsync(user.Id, resource.Id, TestContext.Current.CancellationToken);

        // Assert
        result.IsAuthorized.ShouldBeTrue();
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldReturnAll_WhenUserIsAdmin()
    {
        // Arrange
        await using var scope = FeedCompositionRoot.BeginLifetimeScope();
        await using var context = scope.Resolve<FakeDbContext>();
        var factory = scope.Resolve<IDbConnectionFactory>();

        var user1 = AuthorFactory.Create(isPublic: false);
        var user2 = AuthorFactory.Create(isPublic: true);

        var adminId = AuthorId.New();

        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2),
        }.AsQueryable();

        var permissionManager = Substitute.For<IPermissionManager>();
        permissionManager
            .UserIsInRole(adminId.Value, (int)Roles.AdminAuthor, TestContext.Current.CancellationToken)
            .Returns(true);

        var handler = new FakeProfileAuthorizationHandler(context, permissionManager);

        // Act
        var result = await handler.AuthorizeQueryable(resources, adminId, TestContext.Current.CancellationToken);

        // Assert
        result.Count().ShouldBe(resources.Count());
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldFilterOwnedAndPublics_WhenUserIsNotAdmin()
    {
        // Arrange
        await using var scope = FeedCompositionRoot.BeginLifetimeScope();
        var handler = scope.Resolve<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>();

        var owner = AuthorFactory.Create(isPublic: false);
        var user1 = AuthorFactory.Create(isPublic: true);
        var user2 = AuthorFactory.Create(isPublic: false);

        var options = new AuthOptions(IsAdmin: false);

        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(owner),
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2)
        }.AsQueryable();

        // Act
        var result = await handler.AuthorizeQueryable(resources, owner.Id, TestContext.Current.CancellationToken);

        // Assert
        result.Count().ShouldBe(2);
        result.All(x => x.Author.ProfileIsPublic || x.AuthorId == owner.Id).ShouldBeTrue();
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldFilterFollowersAndPublics_WhenUserIsNotAdmin()
    {
        // Arrange
        await using var scope = FeedCompositionRoot.BeginLifetimeScope();
        var context = scope.Resolve<FakeDbContext>();
        var handler = scope.Resolve<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>(); 

        var options = new AuthOptions(IsAdmin: false);
        var user1 = AuthorFactory.Create(isPublic: false);
        var user2 = AuthorFactory.Create(isPublic: false);
        var user3 = AuthorFactory.Create(isPublic: false);
        var user4 = AuthorFactory.Create(isPublic: false);
        var follower = AuthorFactory.Create(isPublic: false);

        var follows = new List<Follow>
        {
            Follow.Create(follower.Id, user1.Id, DateTimeOffset.UtcNow, FollowStatus.Following),
            Follow.Create(follower.Id, user2.Id, DateTimeOffset.UtcNow, FollowStatus.Following),
        };

        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2),
            FakeUserResource.Create(user3),
            FakeUserResource.Create(user4)
        };

        context.AddRange(user1, user2, user3, user4, follower);
        context.AddRange(follows);
        context.AddRange(resources);
        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        // Act
        var querayble = await handler.AuthorizeQueryable(
            context.Set<FakeUserResource>(),
            follower.Id, 
            TestContext.Current.CancellationToken);

        var result = await querayble
            .Include(x => x.Author)
            .ThenInclude(x => x.Followers)
            .ToListAsync(TestContext.Current.CancellationToken);

        // Assert
        // The public check here is for concurrency sake among the class fixture instance
        // Where the database might have users with public profiles from other tests
        result.All(x => x.Author.Followers.Any(u => u.FollowerId == follower.Id) || x.Author.ProfileIsPublic)
            .ShouldBeTrue();

        result.Where(x => x.Author.Followers.Any(u => u.FollowerId == follower.Id))
            .Count().ShouldBe(2);
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldFilterOnlyPublic_WhenAnonymousUser()
    {
        // Arrange
        await using var scope = FeedCompositionRoot.BeginLifetimeScope();
        var handler = scope.Resolve<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>();

        AuthorId ? anonymousAuthorId = null;
        var options = new AuthOptions(IsAdmin: true);
        var user1 = AuthorFactory.Create(isPublic: true);
        var user2 = AuthorFactory.Create(isPublic: false);

        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2),
        }.AsQueryable();

        // Act
        var result = await handler.AuthorizeQueryable(resources, anonymousAuthorId, TestContext.Current.CancellationToken);

        // Assert
        result.Single().Author.ProfileIsPublic.ShouldBeTrue();
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldReturnResource_WhenResourceIdExists()
    {
        // Arrange
        await using var scope = FeedCompositionRoot.BeginLifetimeScope();
        await using var context = scope.Resolve<FakeDbContext>();
        var permissionManager = scope.Resolve<IPermissionManager>();

        var options = new AuthOptions();
        var owner = AuthorFactory.Create(isPublic: true);
        var user1 = AuthorFactory.Create(isPublic: true);
        var user2 = AuthorFactory.Create(isPublic: false);
        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(owner),
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2),
        }.AsQueryable();

        var resourceId = resources.First().Id;

        var handler = Substitute.ForPartsOf<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>(context, permissionManager);
        handler.AuthorizeQueryable(
            Arg.Any<IQueryable<FakeUserResource>>(),
            Arg.Any<AuthorId?>(),
            TestContext.Current.CancellationToken)
                .Returns(info => info.Arg<IQueryable<FakeUserResource>>());

        // Act
        var result = await handler.AuthorizeQueryable(resources, owner.Id, resourceId, TestContext.Current.CancellationToken);

        // Assert
        result.Single().Id.ShouldBe(resourceId);
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldNotReturnResource_WhenResourceIdDoesntExist()
    {
        // Arrange
        await using var scope = FeedCompositionRoot.BeginLifetimeScope();
        await using var context = scope.Resolve<FakeDbContext>();
        var permissionManager = scope.Resolve<IPermissionManager>();

        var options = new AuthOptions();
        var nonExistentResourceId = FakeUserResourceId.New();
        var owner = AuthorFactory.Create(isPublic: true);
        var user1 = AuthorFactory.Create(isPublic: true);
        var user2 = AuthorFactory.Create(isPublic: false);
        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(owner),
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2),
        }.AsQueryable();

        var handler = Substitute.ForPartsOf<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>(context, permissionManager);
        handler.AuthorizeQueryable(
            Arg.Any<IQueryable<FakeUserResource>>(),
            Arg.Any<AuthorId?>(),
            TestContext.Current.CancellationToken)
                .Returns(info => info.Arg<IQueryable<FakeUserResource>>());

        // Act
        var result = await handler.AuthorizeQueryable(resources, owner.Id, nonExistentResourceId, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeEmpty();
    }
}


