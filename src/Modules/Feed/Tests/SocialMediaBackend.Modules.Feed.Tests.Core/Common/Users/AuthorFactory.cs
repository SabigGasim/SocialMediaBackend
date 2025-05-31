using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.BuildingBlocks.Tests;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Tests.Core.Common.Users;

public static class AuthorFactory
{
    public static Author Create(string? username = null, string nickname = "user", bool isPublic = true)
    {
        username ??= TextHelper.CreateRandom(8);

        return Author.Create(AuthorId.New(), username, nickname, Media.Create(Media.DefaultProfilePicture.Url), isPublic, 0, 0);
    }
}
