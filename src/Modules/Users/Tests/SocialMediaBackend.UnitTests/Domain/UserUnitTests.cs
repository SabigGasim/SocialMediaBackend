using NSubstitute;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.Modules.Users.Domain.Services;
using SocialMediaBackend.Modules.Users.Domain.Users;
using Shouldly;
using SocialMediaBackend.Modules.Users.Domain.Common.Exceptions;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Users.Tests.Core.Common;
using SocialMediaBackend.Modules.Users.Tests.Core.Common.Users;
using SocialMediaBackend.Modules.Feed.Domain.Follows;
using SocialMediaBackend.Modules.Chat.Domain.Follows;

namespace SocialMediaBackend.Modules.Users.Tests.UnitTests.Domain;

public class UserUnitTests(AuthFixture auth, App app) : AppTestBase(auth, app)
{
    private readonly App App = app;

    [Fact]
    public async Task CreateAsync_ShouldReturnUser()
    {
        //Arrange
        var username = "username";
        var nickname = "nickname";
        var dateOfBirth = new DateOnly(2000, 1, 1);
        var profilePicture = Media.Create("https://noice.com", "path.png");
        var checker = Substitute.For<IUserExistsChecker>();
        checker.CheckAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(false);

        //Act
        var user = await User.CreateAsync(username, nickname, dateOfBirth, checker, profilePicture, ct: TestContext.Current.CancellationToken);

        //Assert
        user.Username.ShouldBe(username);
        user.Nickname.ShouldBe(nickname);
        user.DateOfBirth.ShouldBe(dateOfBirth);
        user.ProfilePicture.ShouldBe(profilePicture);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowBusinessRuleException_WhenUsernameExists()
    {
        //Arrange
        var service = Substitute.For<IUserExistsChecker>();
        service.CheckAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(true);

        //Act & Assert
        await UserFactory
            .CreateAsync(userExistsChecker: service, ct: TestContext.Current.CancellationToken)
            .ShouldThrowAsync<BusinessRuleValidationException>();
    }

    [Fact]
    public async Task FollowOrRequestFollow_ShouldReturnFollowingStatus()
    {
        //Arrange
        var follower = await UserFactory.CreateAsync(ct: TestContext.Current.CancellationToken);
        var user = await UserFactory.CreateAsync(isPublic: true, ct: TestContext.Current.CancellationToken);

        //Act
        var follow = user.FollowOrRequestFollow(follower.Id);

        //Assert
        follow.ShouldNotBeNull();
        follow.FollowerId.ShouldBe(follower.Id);
        follow.FollowingId.ShouldBe(user.Id);
        follow.Status.ShouldBe(FollowStatus.Following);

        user.DomainEvents!.Count.ShouldBe(1);
        user.DomainEvents!.Single().ShouldBeOfType<UserFollowedEvent>();

        var followEvent = (UserFollowedEvent)user.DomainEvents.Single();

        followEvent.FollowerId.ShouldBe(follower.Id);
        followEvent.FollowingId.ShouldBe(user.Id);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task FollowOrRequestFollow_ShouldBeNull_WhenFollowIsAlreadyFollowed(bool isPublic)
    {
        //Arrange
        var user = await UserFactory.CreateAsync(isPublic: isPublic, ct: TestContext.Current.CancellationToken);
        var follower = await UserFactory.CreateAsync(ct: TestContext.Current.CancellationToken);
        user.FollowOrRequestFollow(follower.Id);
        user.ClearDomainEvents();

        //Act
        var follow = user.FollowOrRequestFollow(follower.Id);

        //Assert
        follow.ShouldBeNull();
        user.DomainEvents?.ShouldBeEmpty();
    }

    [Fact]
    public async Task FollowOrRequestFollow_ShouldReturnPendingStatus()
    {
        //Arrange
        var follower = await UserFactory.CreateAsync(ct: TestContext.Current.CancellationToken);
        var user = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);

        //Act
        var follow = user.FollowOrRequestFollow(follower.Id);

        //Assert
        follow.ShouldNotBeNull();
        follow.Status.ShouldBe(FollowStatus.Pending);
        follow.FollowerId.ShouldBe(follower.Id);
        follow.FollowingId.ShouldBe(user.Id);
    }

    [Fact]
    public async Task AcceptFollowRequest_ShouldWork()
    {
        //Arrange
        using var scope = App.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FakeDbContext>();

        var (user, follower, follow) = await context.CreateUserWithFollowerAsync(FollowStatus.Pending);

        //Act
        var accepted = user.AcceptFollowRequest(follower.Id);

        //Assert
        accepted.ShouldBeTrue();
        follow.Status.ShouldBe(FollowStatus.Following);

        user.DomainEvents!.Count.ShouldBe(1);
        user.DomainEvents!.First().ShouldBeOfType<FollowRequestAcceptedEvent>();

        var followEvent = (FollowRequestAcceptedEvent)user.DomainEvents.Single();
        followEvent.FollowerId.ShouldBe(follower.Id);
        followEvent.FollowingId.ShouldBe(user.Id);
    }

    [Fact]
    public async Task AcceptFollowRequest_ShouldNotWork_WhenPendingRequestDoesntExists()
    {
        //Arrange
        using var scope = App.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FakeDbContext>();

        var user = await UserFactory.CreateAsync(isPublic: false, ct: TestContext.Current.CancellationToken);
        var randomUserId = UserId.New();

        context.Add(user);
        await context.SaveChangesAsync(TestContext.Current.CancellationToken);

        //Act
        var accepted = user.AcceptFollowRequest(randomUserId);

        //Assert
        accepted.ShouldBeFalse();
        user.DomainEvents.ShouldBeNull();
    }

    [Fact]
    public async Task AcceptAllPendingFollowRequests_ShouldWork_AndAddDomainEvents_WhenRequestsExists()
    {
        //Arrange
        using var scope = App.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FakeDbContext>();

        var (user, followers, requests) = await context
            .CreateUserWithFollowerAsync(FollowStatus.Pending, followersCount: 3);

        //Act
        var accepted = user.AcceptAllPendingFollowRequests();

        //Assert
        accepted.ShouldBeTrue();
        user.Followers.Count.ShouldBe(3);
        user.DomainEvents!.Count.ShouldBe(3);
        user.DomainEvents.ShouldAllBe(x => x is FollowRequestAcceptedEvent);

        var follows = user.DomainEvents
            .Cast<FollowRequestAcceptedEvent>()
            .Select(e => new { e.FollowerId, e.FollowingId })
            .ToList();

        follows[0].FollowerId.ShouldBe(followers[0].Id);
        follows[1].FollowerId.ShouldBe(followers[1].Id);
        follows[2].FollowerId.ShouldBe(followers[2].Id);

        follows.Select(x => x.FollowingId).ShouldAllBe(x => x == user.Id);
    }

    [Fact]
    public async Task Unfollow_ShouldWork_AndAddDomainEvent_WhenFollowingExists()
    {
        // Arrange
        using var scope = App.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FakeDbContext>();

        var (user, follower, _) = await context.CreateUserWithFollowerAsync(FollowStatus.Following);

        // Act
        var result = follower.Unfollow(user.Id);

        // Assert
        result.ShouldBeTrue();
        follower.Followings.Any(f => f.FollowingId == user.Id)
            .ShouldBeFalse();
        follower.DomainEvents!.Count.ShouldBe(1);
        follower.DomainEvents.First().ShouldBeOfType<UserUnfollowedEvent>();

        var domainEvent = (UserUnfollowedEvent)follower.DomainEvents.Single();
        domainEvent.FollowingId.ShouldBe(user.Id);
        domainEvent.FollowerId.ShouldBe(follower.Id);
    }

    [Fact]
    public async Task Unfollow_ShouldNotWork_WhenFollowerDoesntExist()
    {
        //Arrange
        var user = await UserFactory.CreateAsync(ct: TestContext.Current.CancellationToken);
        var randomUserId = UserId.New();

        //Act
        var accepted = user.Unfollow(randomUserId);

        //Assert
        accepted.ShouldBeFalse();
        user.DomainEvents.ShouldBeNull();
    }

    [Fact]
    public async Task RejectPendingFollowRequest_ShouldWork_AndAddDomainEvent_WhenRequestExists()
    {
        // Arrange
        using var scope = App.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FakeDbContext>();

        var (user, follower, _) = await context.CreateUserWithFollowerAsync(FollowStatus.Pending);

        // Act
        var result = user.RejectPendingFollowRequest(follower.Id);

        // Assert
        result.ShouldBeTrue();
        user.Followers.Any(f => f.FollowerId == follower.Id)
            .ShouldBeFalse();
        user.DomainEvents!.Count.ShouldBe(1);
        user.DomainEvents.ShouldAllBe(x => x is FollowRequestRejectedEvent);

        var domainEvent = (FollowRequestRejectedEvent)user.DomainEvents.Single();
        domainEvent.FollowerId.ShouldBe(follower.Id);
        domainEvent.FollowingId.ShouldBe(user.Id);
    }

    [Fact]
    public async Task RejectPendingFollowRequest_ShouldNotWork_WhenRequestDoesntExist()
    {
        //Arrange
        var user = await UserFactory.CreateAsync(ct: TestContext.Current.CancellationToken);
        var randomUserId = UserId.New();

        //Act
        var accepted = user.RejectPendingFollowRequest(randomUserId);

        //Assert
        accepted.ShouldBeFalse();
        user.DomainEvents.ShouldBeNull();
    }

    [Fact]
    public async Task ChangeUsernameAsync_ShouldReturnTrue_AndUpdateUsername_WhenUsernameIsUnique()
    {
        // Arrange
        var user = await UserFactory.CreateAsync(ct: TestContext.Current.CancellationToken);
        var username = Guid.NewGuid().ToString()[..10];
        var checker = Substitute.For<IUserExistsChecker>();
        checker.CheckAsync(Arg.Any<string>(), TestContext.Current.CancellationToken).Returns(false);

        // Act
        var result = await user.ChangeUsernameAsync(username, checker, TestContext.Current.CancellationToken);

        // Assert
        result.ShouldBeTrue();
        user.Username.ShouldBe(username);
    }

    [Fact]
    public async Task ChangeUsernameAsync_ShouldReturnFalse_WhenUsernameIsSame()
    {
        // Arrange
        var user = await UserFactory.CreateAsync(ct: TestContext.Current.CancellationToken);
        var username = user.Username;
        var token = TestContext.Current.CancellationToken;

        // Act
        var result = await user.ChangeUsernameAsync(username, Substitute.For<IUserExistsChecker>(), token);

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public async Task ChangeUsernameAsync_ShouldThrowBusinessRuleException_WhenUsernameIsNotUnique()
    {
        // Arrange
        var user = await UserFactory.CreateAsync(ct: TestContext.Current.CancellationToken);
        var username = Guid.NewGuid().ToString()[..10];
        var checker = Substitute.For<IUserExistsChecker>();
        var token = TestContext.Current.CancellationToken;
        checker.CheckAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(true);

        // Act & Assert
        await user.ChangeUsernameAsync(username, checker, token).ShouldThrowAsync<BusinessRuleValidationException>();
    }

    [Fact]
    public async Task ChangeNickname_ShouldReturnTrue_AndUpdateNickname_WhenDifferent()
    {
        // Arrange
        var user = await UserFactory.CreateAsync(ct: TestContext.Current.CancellationToken);
        var nickname = Guid.NewGuid().ToString()[..10];

        // Act
        var result = user.ChangeNickname(nickname);

        // Assert
        result.ShouldBeTrue();
        user.Nickname.ShouldBe(nickname);
    }

    [Fact]
    public async Task ChangeNickname_ShouldReturnFalse_WhenNicknameIsSame()
    {
        // Arrange
        var user = await UserFactory.CreateAsync(ct: TestContext.Current.CancellationToken);
        var nickname = user.Nickname;

        // Act
        var result = user.ChangeNickname(nickname);

        // Assert
        result.ShouldBeFalse();
    }
}
