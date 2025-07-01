using FastEndpoints;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Users;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Feed.Application.Authors.GetAuthor;
using SocialMediaBackend.Modules.Feed.Application.Contracts;
using SocialMediaBackend.Modules.Payments.Application.Contracts;
using SocialMediaBackend.Modules.Payments.Application.Payers.GetPayer;
using SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

namespace SocialMediaBackend.Tests.SystemTests.Users;

public class CreateUserTests(AuthFixture auth, App app) : AppTestBase(auth, app)
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
            AssertEventually(new GetCreatedUserFromPaymentsProbe(payerInfo, new PaymentsModule()), _testTimeout)
        );
    }

    private class GetCreatedUserFromFeedProbe(UserInfo expectedUserInfo, IFeedModule module) : IProbe
    {
        private readonly UserInfo _expectedUserInfo = expectedUserInfo;
        private readonly IFeedModule _module = module;

        private UserInfo _actualUserInfo = default!;

        public bool IsSatisfied()
        {
            return _actualUserInfo == _expectedUserInfo;
        }

        public async Task SampleAsync()
        {
            var res = await _module.ExecuteCommandAsync<GetAuthorCommand, GetAuthorResponse>(new GetAuthorCommand(_expectedUserInfo.Id));

            if (!res.IsSuccess)
            {
                return;
            }

            var author = res.Payload;

            _actualUserInfo = new UserInfo(
                author.Id,
                author.Username,
                author.Nickname,
                author.ProfilePictureUrl!);
        }

        public string DescribeFailureTo()
        {
            return $"Author with ID {_expectedUserInfo.Id} was not created in feed module";
        } 
    }

    private class GetCreatedUserFromChatProbe(UserInfo expectedUserInfo, IChatModule module) : IProbe
    {
        private readonly UserInfo _expectedUserInfo = expectedUserInfo;
        private readonly IChatModule _module = module;

        private UserInfo _actualUserInfo = default!;

        public bool IsSatisfied()
        {
            return _actualUserInfo == _expectedUserInfo;
        }

        public async Task SampleAsync()
        {
            var res = await _module.ExecuteCommandAsync<GetChatterCommand, GetChatterResponse>(new GetChatterCommand(_expectedUserInfo.Id));

            if (!res.IsSuccess)
            {
                return;
            }

            var author = res.Payload;

            _actualUserInfo = new UserInfo(
                author.Id,
                author.Username,
                author.Nickname,
                author.ProfilePictureUrl!);
        }

        public string DescribeFailureTo()
        {
            return $"Chatter with ID {_expectedUserInfo.Id} was not created in chat module";
        }
    }

    private class GetCreatedUserFromPaymentsProbe(PayerInfo expectedPayerInfo, IPaymentsModule module) : IProbe
    {
        private readonly PayerInfo _expectedPayerInfo = expectedPayerInfo;
        private readonly IPaymentsModule _module = module;

        private PayerInfo _actualUserInfo = default!;

        public bool IsSatisfied()
        {
            return _actualUserInfo == _expectedPayerInfo;
        }

        public async Task SampleAsync()
        {
            var res = await _module.ExecuteCommandAsync<GetPayerCommand, GetPayerResponse>(new GetPayerCommand(_expectedPayerInfo.Id));

            if (!res.IsSuccess)
            {
                return;
            }

            var payer = res.Payload;

            _actualUserInfo = new PayerInfo(
                payer.Id,
                payer.IsDeleted
                );
        }

        public string DescribeFailureTo()
        {
            return $"Payer with ID {_expectedPayerInfo.Id} was not created in payments module";
        }
    }

    public record UserInfo(Guid Id, string Username, string Nickname, string ProfilePicture);
    public record PayerInfo(Guid Id, bool IsDeleted);
}
