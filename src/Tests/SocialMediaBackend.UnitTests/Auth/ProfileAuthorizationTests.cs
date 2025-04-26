using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Resources;
using NSubstitute;
using Shouldly;
using SocialMediaBackend.Application.Auth;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Users.Follows;
using SocialMediaBackend.Infrastructure.Data;
using Tests.Core.Common;
using Tests.Core.Common.Users;

namespace SocialMediaBackend.Application.UnitTests.Auth;

public class FakeProfileAuthorizationHandler(FakeDbContext context) : ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>(context);

public class ProfileAuthorizationHandlerBaseTests : IDisposable
{
    private readonly FakeDbContext _dbContext;
    private readonly FakeProfileAuthorizationHandler _handler;

    public ProfileAuthorizationHandlerBaseTests()
    {
        _dbContext = CreateInMemoryDbContext();
        _handler = new FakeProfileAuthorizationHandler(_dbContext);
    }

    [Fact]
    public async Task AuthorizeAsync_ShouldReturnTrue_WhenUserIsOwner()
    {
        // Arrange
        var user = await UserFactory.CreateAsync();
        var resource = FakeUserResource.Create(user);
        _dbContext.Add(resource);

        await _dbContext.SaveChangesAsync();

        var options = new AuthOptions(IsAdmin: false);

        // Act
        var result = await _handler.AuthorizeAsync(user.Id, resource.Id, options);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task AuthorizeAsync_ShouldReturnFalse_WhenProfileIsPrivate_AndNotAdmin()
    {
        // Arrange
        var owner = await UserFactory.CreateAsync(isPublic: false);
        var resource = FakeUserResource.Create(owner);
        _dbContext.Add(resource);

        await _dbContext.SaveChangesAsync();

        var differentUserId = new UserId(Guid.NewGuid());
        var options = new AuthOptions(IsAdmin: false);

        // Act
        var result = await _handler.AuthorizeAsync(differentUserId, resource.Id, options);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public async Task IsAdminOrResourceOwnerAsync_ShouldReturnTrue_WhenUserIsAdmin()
    {
        // Arrange
        var userId = new UserId(Guid.NewGuid());
        var resourceId = Guid.NewGuid();
        var options = new AuthOptions { IsAdmin = true };

        // Act
        var result = await _handler.IsAdminOrResourceOwnerAsync(userId, new(resourceId), options);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task IsAdminOrResourceOwnerAsync_ShouldReturnTrue_WhenUserIsOwner()
    {
        // Arrange
        var user = await UserFactory.CreateAsync(isPublic: false);
        var resource = FakeUserResource.Create(user);
        _dbContext.Add(resource);

        await _dbContext.SaveChangesAsync();

        var options = new AuthOptions { IsAdmin = false };

        // Act
        var result = await _handler.IsAdminOrResourceOwnerAsync(user.Id, resource.Id, options);

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldReturnAll_WhenUserIsAdmin()
    {
        // Arrange
        var user1 = await UserFactory.CreateAsync(isPublic: false);
        var user2 = await UserFactory.CreateAsync(isPublic: true);

        var adminId = UserId.New();
        var options = new AuthOptions(IsAdmin: true);

        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2),
        }.AsQueryable();

        // Act
        var result = _handler.AuthorizeQueryable(resources, adminId, options).ToList();

        // Assert
        result.Count.ShouldBe(2);
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldFilterPublics_WhenUserIsNotAdmin()
    {
        // Arrange
        var owner = await UserFactory.CreateAsync(isPublic: false);
        var user2 = await UserFactory.CreateAsync(isPublic: true);
        var user3 = await UserFactory.CreateAsync(isPublic: false);

        var userId = new UserId(Guid.NewGuid());
        var options = new AuthOptions(IsAdmin: false);

        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(owner),
            FakeUserResource.Create(user2),
            FakeUserResource.Create(user3)
        }.AsQueryable();

        // Act
        var result = _handler.AuthorizeQueryable(resources, owner.Id, options).ToList();

        // Assert
        result.Count.ShouldBe(2);
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldFilterFollowers_WhenUserIsNotAdmin()
    {
        // Arrange
        var options = new AuthOptions(IsAdmin: false);
        var user1 = await UserFactory.CreateAsync(isPublic: false);
        var user2 = await UserFactory.CreateAsync(isPublic: false);
        var user3 = await UserFactory.CreateAsync(isPublic: false);
        var owner = await UserFactory.CreateAsync(isPublic: false);

        var follows = new List<Follow> 
        {
            Follow.Create(owner.Id, user1.Id),
            Follow.Create(owner.Id, user2.Id),
        };

        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(owner),
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2),
            FakeUserResource.Create(user3)
        };

        _dbContext.AddRange(user1, user2, user3, owner);
        _dbContext.AddRange(follows);
        _dbContext.AddRange(resources);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _handler
            .AuthorizeQueryable(_dbContext.Set<FakeUserResource>(), owner.Id, options)
            .ToListAsync();

        // Assert
        result.Count.ShouldBe(3);
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldFilterOnlyPublic_WhenAnonymousUser()
    {
        // Arrange
        UserId? anonymousUserId = null;
        var options = new AuthOptions(IsAdmin: true);
        var user1 = await UserFactory.CreateAsync(isPublic: true);
        var user2 = await UserFactory.CreateAsync(isPublic: false);

        var resources = new List<FakeUserResource>
        {
            FakeUserResource.Create(user1),
            FakeUserResource.Create(user2),
        }.AsQueryable();

        // Act
        var result = _handler.AuthorizeQueryable(resources, anonymousUserId, options).ToList();

        // Assert
        result.Count.ShouldBe(1);
        result[0].User.ProfileIsPublic.ShouldBeTrue();
    }

    [Fact]
    public async Task AuthorizeQueryable_ShouldReturnResource_WhenResourceIdExists()
    {
        // Arrange
        var options = new AuthOptions();
        var owner = await UserFactory.CreateAsync(isPublic: true);
        var resources = new List<FakeUserResource> { FakeUserResource.Create(owner) }.AsQueryable();
        var resourceId = resources.First().Id;

        var handler = Substitute.ForPartsOf<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>(_dbContext);
        handler.AuthorizeQueryable(
            resources, owner.Id, options)
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
        var owner = await UserFactory.CreateAsync(isPublic: true);
        var resources = new List<FakeUserResource> { FakeUserResource.Create(owner) }.AsQueryable();
        var nonExistantResourceId = new FakeUserResourceId(Guid.NewGuid());

        var handler = Substitute.ForPartsOf<ProfileAuthorizationHandlerBase<FakeUserResource, FakeUserResourceId>>(_dbContext);
        handler.AuthorizeQueryable(
            Arg.Any<IQueryable<FakeUserResource>>(),
            Arg.Any<UserId?>(),
            Arg.Any<AuthOptions>())
                .Returns(info => info.Arg<IQueryable<FakeUserResource>>());

        // Act
        var result = handler.AuthorizeQueryable(resources, owner.Id, nonExistantResourceId, new()).ToList();

        // Assert
        result.Count.ShouldBe(0);
    }

    private static FakeDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new FakeDbContext(options);
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
    }
}


