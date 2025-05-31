using SocialMediaBackend.BuildingBlocks.Domain.ValueObjects;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Tests.Core.Common.Posts;

public static class PostFactory
{
    public static Post Create(AuthorId? userId = null, string text = "text", IEnumerable<Media>? mediaItems = null)
    {
        return Post.Create(userId ?? AuthorId.New(), text, mediaItems).Payload;
    }
}
