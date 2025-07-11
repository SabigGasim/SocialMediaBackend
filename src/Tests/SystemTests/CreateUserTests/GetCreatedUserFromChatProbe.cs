using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Chat.Application.Chatters.GetChatter;
using SocialMediaBackend.Modules.Chat.Application.Contracts;

namespace SocialMediaBackend.Tests.SystemTests.CreateUserTests;

internal sealed class GetCreatedUserFromChatProbe(UserInfo expectedUserInfo, IChatModule module) : IProbe
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
        var res = await _module.ExecuteQueryAsync<GetChatterQuery, GetChatterResponse>(new GetChatterQuery(_expectedUserInfo.Id));

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
