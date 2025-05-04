using SocialMediaBackend.Modules.Users.Domain.Common.ValueObjects;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;
using SocialMediaBackend.Modules.Users.Domain.Users;
using Tests.Core.Common.Users;

namespace Tests.Core.Common.Posts;

public static class PostFactory
{
    public static Post Create(UserId? userId = null, string text = "text", IEnumerable<Media>? mediaItems = null)
    {
        return Post.Create(userId ?? UserId.New(), text, mediaItems)!;
    }
}
