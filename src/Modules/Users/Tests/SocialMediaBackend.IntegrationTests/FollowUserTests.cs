﻿using FastEndpoints;
using Shouldly;
using SocialMediaBackend.Modules.Users.Application.Users.CreateUser;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using SocialMediaBackend.Modules.Users.Tests.Core.Common;
using System.Net;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Api.Modules.Users.Endpoints;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Follows;

namespace SocialMediaBackend.Modules.Users.Tests.IntegrationTests;

public class FollowUserTests(App app) : AppTestBase(app)
{
    private readonly App _app = app;
    private static CreateUserRequest CreateUserRequest => new(
            Username: TextHelper.CreateRandom(10),
            Nickname: TextHelper.CreateRandom(10),
            DateOnly.Parse("2000/01/01"),
            Media.DefaultProfilePicture);

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
