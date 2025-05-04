using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;
using SocialMediaBackend.Application.Auth;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Users.Follows;
using Tests.Core.Common;
using Tests.Core.Common.Users;

namespace SocialMediaBackend.UnitTests.Application.Auth;


public class ProfileAuthorizationHandlerBaseTests(AuthFixture auth, App app) : AppTestBase(auth, app)
{
    private readonly App App = app;

    [Fact]
    public async Task AuthorizeAsync_ShouldReturnTrue_WhenUserIsOwner()
    {
        // Arrange
        using var scope = App.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FakeDbContext>();
        var handler = scope.ServiceProvider.GetRequiredService<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>();

        var user = await UserFactory.CreateAsync(ct: TestContext.Current.CancellationToken);
        var resource = FakeUserResource.Create(user);
        context.Add(user);
        context.Add(resource);

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var options = new AuthOptions(IsAdmin: false);

        // Act
        var result = await handler.AuthorizeAsync(user.Id, resource.Id, options, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task AuthorizeAsync_ShouldReturnFalse_WhenProfileIsPrivate_AndNotAdmin()
    {
        // Arrange
        await using var scope = App.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<FakeDbContext>();
        var handler = scope.ServiceProvider.GetRequiredService<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>();

        var owner = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);
        var resource = FakeUserResource.Create(owner);
        context.Add(owner);
        context.Add(resource);

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var differentUserId = UserId.New();
        var options = new AuthOptions(IsAdmin: false);

        // Act
        var result = await handler.AuthorizeAsync(differentUserId, resource.Id, options, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public async Task IsAdminOrResourceOwnerAsync_ShouldReturnTrue_WhenUserIsAdmin()
    {
        // Arrange
        await using var scope = App.Services.CreateAsyncScope();
        var handler = scope.ServiceProvider.GetRequiredService<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>();

        var userId = UserId.New();
        var resourceId = FakeUserResourceId.New();
        var options = new AuthOptions(IsAdmin: true);

        // Act
        var result = await handler.IsAdminOrResourceOwnerAsync(userId, resourceId, options, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task IsAdminOrResourceOwnerAsync_ShouldReturnTrue_WhenUserIsOwner()
    {
        // Arrange
        await using var scope = App.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<FakeDbContext>();
        var handler = scope.ServiceProvider.GetRequiredService<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>();

        var user = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken );
        var resource = FakeUserResource.Create(user);

        context.Add(user);
        context.Add(resource);

        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        var options = new AuthOptions(IsAdmin: false);

        // Act
        var result = await handler.IsAdminOrResourceOwnerAsync(user.Id, resource.Id, options, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldReturnAll_WhenUserIsAdmin()
    {
        // Arrange
        using var scope = App.Services.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>();

        var user1 = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);
        var user2 = await UserFactory.CreateAsync(isPublic: true, ct: TestContext.Current.CancellationToken);

        var adminId = UserId.New();
        var options = new AuthOptions(IsAdmin: true);

        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2),
        }.AsQueryable();

        // Act
        var result = handler.AuthorizeQueryable(resources, adminId, options).ToList();

        // Assert
        result.Count.ShouldBe(resources.Count());
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldFilterOwnedAndPublics_WhenUserIsNotAdmin()
    {
        // Arrange
        using var scope = App.Services.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>();

        var owner = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);
        var user1 = await UserFactory.CreateAsync(isPublic: true, ct: TestContext.Current.CancellationToken);
        var user2 = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);

        var options = new AuthOptions(IsAdmin: false);

        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(owner),
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2)
        }.AsQueryable();

        // Act
        var result = handler.AuthorizeQueryable(resources, owner.Id, options).ToList();

        // Assert
        result.Count.ShouldBe(2);
        result.All(x => x.User.ProfileIsPublic || x.UserId == owner.Id).ShouldBeTrue();
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldFilterFollowersAndPublics_WhenUserIsNotAdmin()
    {
        // Arrange
        await using var scope = App.Services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<FakeDbContext>();
        var handler = scope.ServiceProvider.GetRequiredService<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>();

        var options = new AuthOptions(IsAdmin: false);
        var user1 = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);
        var user2 = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);
        var user3 = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);
        var user4 = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);
        var follower = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);

        var follows = new List<Follow>
        {
            Follow.Create(follower.Id, user1.Id),
            Follow.Create(follower.Id, user2.Id),
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
        var result = await handler
            .AuthorizeQueryable(context.Set<FakeUserResource>(), follower.Id, options)
            .Include(x => x.User)
            .ThenInclude(x => x.Followers)
            .ToListAsync(TestContext.Current.CancellationToken);

        // Assert
        // The public check here is for concurrency sake among the class fixture instance
        // Where the database might have users with public profiles from other tests
        result.All(x => x.User.Followers.Any(u => u.FollowerId == follower.Id) || x.User.ProfileIsPublic)
            .ShouldBeTrue();

        result.Where(x => x.User.Followers.Any(u => u.FollowerId == follower.Id))
            .Count().ShouldBe(2);
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldFilterOnlyPublic_WhenAnonymousUser()
    {
        // Arrange
        using var scope = App.Services.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>();

        UserId? anonymousUserId = null;
        var options = new AuthOptions(IsAdmin: true);
        var user1 = await UserFactory.CreateAsync(isPublic: true, ct: TestContext.Current.CancellationToken);
        var user2 = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);

        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2),
        }.AsQueryable();

        // Act
        var result = handler.AuthorizeQueryable(resources, anonymousUserId, options).ToList();

        // Assert
        result.Count.ShouldBe(1);
        result[0].User.ProfileIsPublic.ShouldBeTrue();
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldReturnResource_WhenResourceIdExists()
    {
        // Arrange
        using var scope = App.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FakeDbContext>();

        var options = new AuthOptions();
        var owner = await UserFactory.CreateAsync(isPublic: true, ct: TestContext.Current.CancellationToken);
        var user1 = await UserFactory.CreateAsync(isPublic: true, ct: TestContext.Current.CancellationToken);
        var user2 = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);
        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(owner),
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2),
        }.AsQueryable();

        var resourceId = resources.First().Id;

        var handler = Substitute.ForPartsOf<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>(context);
        handler.AuthorizeQueryable(
            Arg.Any<IQueryable<FakeUserResource>>(),
            Arg.Any<UserId?>(),
            Arg.Any<AuthOptions>())
                .Returns(info => info.Arg<IQueryable<FakeUserResource>>());

        // Act
        var result = handler.AuthorizeQueryable(resources, owner.Id, resourceId, options).ToList();

        // Assert
        result.Count.ShouldBe(1);
        result[0].Id.ShouldBe(resourceId);
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldNotReturnResource_WhenResourceIdDoesntExist()
    {
        // Arrange
        using var scope = App.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FakeDbContext>();

        var options = new AuthOptions();
        var nonExistentResourceId = FakeUserResourceId.New();
        var owner = await UserFactory.CreateAsync(isPublic: true, ct: TestContext.Current.CancellationToken);
        var user1 = await UserFactory.CreateAsync(isPublic: true, ct: TestContext.Current.CancellationToken);
        var user2 = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);
        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(owner),
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2),
        }.AsQueryable();

        var handler = Substitute.ForPartsOf<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>(context);
        handler.AuthorizeQueryable(
            Arg.Any<IQueryable<FakeUserResource>>(),
            Arg.Any<UserId?>(),
            Arg.Any<AuthOptions>())
                .Returns(info => info.Arg<IQueryable<FakeUserResource>>());

        // Act
        var result = handler.AuthorizeQueryable(resources, owner.Id, nonExistentResourceId, options).ToList();

        // Assert
        result.Count.ShouldBe(0);
    }
}


