using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Entities;

public record PostLike : ValueObject
{
    public Guid UserId { get; private set; }
    public Guid PostId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public PostLike(Guid userId, Guid postId, DateTimeOffset createdAt)
    {
        UserId = userId;
        PostId = postId;
        CreatedAt = createdAt;
    }

    protected override IEnumerable<object> GetComponents()
    {
        yield return UserId;
        yield return PostId;
        yield return CreatedAt;
    }
}