using SocialMediaBackend.Domain.Common.ValueObjects;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;
using Tests.Core.Common.Users;

namespace Tests.Core.Common.Posts;

public static class PostFactory
{
    public static Post Create(string text = "text", IEnumerable<Media>? mediaItems = null)
    {
        return Post.Create(UserId.New(), text, mediaItems)!;
    }
}
