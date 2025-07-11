using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Feed.Application.Authors.GetAuthor;
using SocialMediaBackend.Modules.Feed.Application.Contracts;

namespace SocialMediaBackend.Tests.SystemTests.CreateUserTests;

internal sealed class GetCreatedUserFromFeedProbe(UserInfo expectedUserInfo, IFeedModule module) : IProbe
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
        var res = await _module.ExecuteQueryAsync<GetAuthorQuery, GetAuthorResponse>(new GetAuthorQuery(_expectedUserInfo.Id));

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
