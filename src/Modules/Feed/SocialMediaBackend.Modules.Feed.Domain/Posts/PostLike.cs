using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Domain.Posts;

public record PostLike : ValueObject
{
    public AuthorId UserId { get; private set; } = default!;
    public PostId PostId { get; private set; } = default!;
    public DateTimeOffset Created { get; private set; }
    public Post Post { get; private set; } = default!;

    private PostLike(AuthorId userId, PostId postId)
    {
        UserId = userId;
        PostId = postId;
        Created = DateTimeOffset.UtcNow;
    }

    private PostLike() { }

    internal static PostLike Create(AuthorId userId, PostId postId)
    {
        return new PostLike(userId, postId);
    }

    protected override IEnumerable<object> GetComponents()
    {
        yield return UserId;
        yield return PostId;
        yield return Created;
    }
}