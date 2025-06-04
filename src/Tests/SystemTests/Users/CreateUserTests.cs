using FastEndpoints;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Users;
using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;
using SocialMediaBackend.Modules.Chat.Application.Contracts;
using SocialMediaBackend.Modules.Feed.Application.Authors.GetAuthor;
using SocialMediaBackend.Modules.Feed.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.CreateUser;
using SocialMediaBackend.Tests.SystemTests;

namespace SocialMediaBackend.Tests.Tests.IntegrationTests.Api.Users;

public class CreateUserTests(AuthFixture auth, App app) : AppTestBase(auth, app)
{
    private readonly App _app = app;

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

        await Task.WhenAll(
            AssertEventually(new GetCreatedUserFromChatProbe(userInfo, new ChatModule()), TimeSpan.FromSeconds(10)),
            AssertEventually(new GetCreatedUserFromFeedProbe(userInfo, new FeedModule()), TimeSpan.FromSeconds(10))
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

    public record UserInfo(Guid Id, string Username, string Nickname, string ProfilePicture);
}
