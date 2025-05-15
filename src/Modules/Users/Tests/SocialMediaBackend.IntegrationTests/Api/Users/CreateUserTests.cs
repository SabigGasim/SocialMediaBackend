using FastEndpoints;
using Shouldly;
using SocialMediaBackend.Modules.Users.Application.Users.CreateUser;
using SocialMediaBackend.Modules.Users.Application.Users.DeleteUser;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using SocialMediaBackend.Modules.Users.Tests.Core.Common;
using System.Net;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Users;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Users.Follows;
using SocialMediaBackend.Modules.Feed.Domain.Follows;

namespace SocialMediaBackend.Modules.Users.Tests.IntegrationTests.Api.Users;

public class UserTests(AuthFixture auth, App app) : AppTestBase(auth, app)
{
    private readonly App App = app;
    private static CreateUserRequest CreateUserRequest => new(
            Username: TextHelper.CreateRandom(10),
            Nickname: TextHelper.CreateRandom(10),
            DateOnly.Parse("2000/01/01"),
            Media.DefaultProfilePicture);

    [Fact]
    public async Task CreateUserEndpoint_ShouldWork()
    {
        var request = CreateUserRequest;

        var (rsp, user) = await App.Client.POSTAsync<CreateUserEndpoint, CreateUserRequest, CreateUserResponse>(request);

        rsp.IsSuccessStatusCode.ShouldBeTrue();
        rsp.StatusCode.ShouldBe(HttpStatusCode.Created);

        user.Username.ShouldBe(user.Username);
        user.Nickname.ShouldBe(user.Nickname);
        user.DateOfBirth.ShouldBe(user.DateOfBirth);
        user.ProfilePicture.ShouldBe(user.ProfilePicture);
    }

    [Fact]
    public async Task DeleteUserEndpoint_ShouldWork_WhenUserExists()
    {
        var request = CreateUserRequest;

        var (_, user) = await App.Client.POSTAsync<CreateUserEndpoint, CreateUserRequest, CreateUserResponse>(request);
        var rsp = await App.Client.DELETEAsync<DeleteUserEndpoint, DeleteUserRequest>(new(user.Id));

        rsp.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        rsp.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Fact]
    public async Task FollowUserEndpoint_ShouldWork_WhenUserExists()
    {
        var request = CreateUserRequest;

        var (_, userToFollow) = await App.Client.POSTAsync<CreateUserEndpoint, CreateUserRequest, CreateUserResponse>(request);
        var (rsp, res) = await App.Client.POSTAsync<FollowUserEndpoint, FollowUserRequest, FollowUserResponse>(new(userToFollow.Id));

        rsp.IsSuccessStatusCode.ShouldBeTrue();
        rsp.StatusCode.ShouldBe(HttpStatusCode.Created);
        res.FollowStatus.ShouldBe(FollowStatus.Following);
    }

    [Theory]
    public async Task FollowUserEndpoint_ShouldFail_WhenUserDoesntExist(int x)
    {
        var request = CreateUserRequest;

        var (_, userToFollow) = await App.Client.POSTAsync<CreateUserEndpoint, CreateUserRequest, CreateUserResponse>(request);
        var (rsp, res) = await App.Client.POSTAsync<FollowUserEndpoint, FollowUserRequest, FollowUserResponse>(new(userToFollow.Id));
        var mediator = App.Context.< MediatorDecorator > ;
        TestContext.Current.

        rsp.IsSuccessStatusCode.ShouldBeTrue();
        rsp.StatusCode.ShouldBe(HttpStatusCode.Created);
        res.FollowStatus.ShouldBe(FollowStatus.Following);
        var followEvent = mediator.PublishedEvents.Single().ShouldBeOfType<UserFollowedEvent>();
        followEvent.FollowingId.ShouldBe(new(userToFollow.Id));
        followEvent.FollowerId.ShouldBe(AdminId);
    }
}
