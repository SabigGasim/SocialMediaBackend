using SocialMediaBackend.Modules.Users.Domain.Common;

namespace SocialMediaBackend.Modules.Users.Domain.Feed.Comments;


public record CommentLike : ValueObject
{
    public AuthorId UserId { get; private set; } = default!;
    public CommentId CommentId { get; private set; } = default!;
    public Comment Comment { get; private set; } = default!;
    public DateTimeOffset Created { get; private set; }

    private CommentLike(AuthorId userId, CommentId commentId)
    {
        UserId = userId;
        CommentId = commentId;
        Created = DateTimeOffset.UtcNow;
    }

    private CommentLike() { }

    internal static CommentLike Create(AuthorId userId, CommentId commentId)
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