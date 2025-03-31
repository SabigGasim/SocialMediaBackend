using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Entities;


public record CommentLike : ValueObject
{
    public Guid UserId { get; private set; }
    public Guid CommentId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    public CommentLike(Guid userId, Guid commentId)
    {
        UserId = userId;
        CommentId = commentId;
    }

    protected override IEnumerable<object> GetComponents()
    {
        yield return UserId;
        yield return CommentId;
        yield return CreatedAt;
    }
}