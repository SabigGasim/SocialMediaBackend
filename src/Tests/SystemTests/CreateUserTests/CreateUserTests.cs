using FastEndpoints;
using SocialMediaBackend.Api.Modules.Users.Endpoints;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.AppSubscriptions.Application.Contracts;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Feed.Application.Contracts;
using SocialMediaBackend.Modules.Payments.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

namespace SocialMediaBackend.Tests.SystemTests.CreateUserTests;

public class CreateUserTests(App app) : AppTestBase(app)
{
    private readonly App _app = app;

    private readonly TimeSpan _testTimeout = TimeSpan.FromSeconds(20);

    [Fact]
    public async Task CreateUser_ShouldCreateUserInAllModules()
    {
        var request = new CreateUserRequest(
            Username: TextHelper.CreateRandom(10),
            Nickname: TextHelper.CreateRandom(10),
            DateOnly.Parse("2000/01/01"),
            Media.DefaultProfilePicture);

        var (rsp, user) = await _app.Client.POSTAsync<
            CreateUserEndpoint, 
            CreateUserRequest, 
            CreateUserResponse>(request);

        rsp.EnsureSuccessStatusCode();

        var userInfo = new UserInfo(
            user.Id,
            user.Username,
            user.Nickname,
            user.ProfilePicture.Url);

        var payerInfo = new PayerInfo(user.Id, false);

        await Task.WhenAll(
            AssertEventually(new GetCreatedUserFromChatProbe(userInfo, new ChatModule()), _testTimeout),
            AssertEventually(new GetCreatedUserFromFeedProbe(userInfo, new FeedModule()), _testTimeout),
            AssertEventually(new GetCreatedUserFromPaymentsProbe(payerInfo, new PaymentsModule()), _testTimeout),
            AssertEventually(new GetCreatedUserFromAppSubscriptionsProbe(user.Id, new AppSubscriptionsModule()), _testTimeout)
        );
    }
}
