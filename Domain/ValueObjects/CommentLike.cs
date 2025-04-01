using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Entities;


public record CommentLike : ValueObject
{
    public Guid UserId { get; private set; }
    public Guid CommentId { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private CommentLike(Guid userId, Guid commentId)
    {
        UserId = userId;
        CommentId = commentId;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    private CommentLike() { }

    internal static CommentLike Create(Guid userId, Guid commentId)
    {
        return new CommentLike(userId, commentId);
    }

    protected override IEnumerable<object> GetComponents()
    {
        yield return UserId;
        yield return CommentId;
        yield return CreatedAt;
    }
}