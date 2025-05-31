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
using SocialMediaBackend.BuildingBlocks.Tests;

namespace SocialMediaBackend.Modules.Users.Tests.IntegrationTests.Api.Users;

public class UserIntegrationTests(AuthFixture auth, App app) : AppTestBase(auth, app)
{
    private readonly App _app = app;
    private static CreateUserRequest CreateUserRequest => new(
            Username: TextHelper.CreateRandom(10),
            Nickname: TextHelper.CreateRandom(10),
            DateOnly.Parse("2000/01/01"),
            Media.DefaultProfilePicture);

    [Fact]
    public async Task CreateUserEndpoint_ShouldWork()
    {
        var request = CreateUserRequest;

        var (rsp, user) = await _app.Client.POSTAsync<CreateUserEndpoint, CreateUserRequest, CreateUserResponse>(request);

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

        var (_, user) = await _app.Client.POSTAsync<CreateUserEndpoint, CreateUserRequest, CreateUserResponse>(request);
        var rsp = await _app.Client.DELETEAsync<DeleteUserEndpoint, DeleteUserRequest>(new(user.Id));

        rsp.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        rsp.IsSuccessStatusCode.ShouldBeTrue();
    }

    [Fact]
    public async Task FollowUserEndpoint_ShouldWork_WhenUserExists()
    {
        var request = CreateUserRequest;

        var (_, userToFollow) = await _app.Client.POSTAsync<CreateUserEndpoint, CreateUserRequest, CreateUserResponse>(request);
        var (rsp, res) = await _app.Client.POSTAsync<FollowUserEndpoint, FollowUserRequest, FollowUserResponse>(new(userToFollow.Id));

        rsp.IsSuccessStatusCode.ShouldBeTrue();
        rsp.StatusCode.ShouldBe(HttpStatusCode.Created);
        res.FollowStatus.ShouldBe(FollowStatus.Following);
    }

    [Fact]
    public async Task FollowUserEndpoint_ShouldFail_WhenUserDoesntExist()
    {
        var request = CreateUserRequest;

        var (_, userToFollow) = await _app.Client.POSTAsync<CreateUserEndpoint, CreateUserRequest, CreateUserResponse>(request);
        var (rsp, res) = await _app.Client.POSTAsync<FollowUserEndpoint, FollowUserRequest, FollowUserResponse>(new(userToFollow.Id));

        rsp.IsSuccessStatusCode.ShouldBeTrue();
        rsp.StatusCode.ShouldBe(HttpStatusCode.Created);
        res.FollowStatus.ShouldBe(FollowStatus.Following);
    }
}
