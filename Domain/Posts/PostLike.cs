using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Posts;

public record PostLike : ValueObject
{
    public Guid UserId { get; private set; }
    public Guid PostId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private PostLike(Guid userId, Guid postId)
    {
        UserId = userId;
        PostId = postId;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    private PostLike() { }

    internal static PostLike Create(Guid userId, Guid id)
    {
        return new PostLike(userId, id);
    }

    protected override IEnumerable<object> GetComponents()
    {
        yield return UserId;
        yield return PostId;
        yield return CreatedAt;
    }
}