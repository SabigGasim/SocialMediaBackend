using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Users;

namespace SocialMediaBackend.Domain.Comments;


public record CommentLike : ValueObject
{
    public UserId UserId { get; private set; } = default!;
    public CommentId CommentId { get; private set; } = default!;
    public DateTimeOffset Created { get; private set; }

    private CommentLike(UserId userId, CommentId commentId)
    {
        UserId = userId;
        CommentId = commentId;
        Created = DateTimeOffset.UtcNow;
    }

    private CommentLike() { }

    internal static CommentLike Create(UserId userId, CommentId commentId)
    {
        return new CommentLike(userId, commentId);
    }

    protected override IEnumerable<object> GetComponents()
    {
        yield return UserId;
        yield return CommentId;
        yield return Created;
    }
}