using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Feed.Comments;
public class CommentDeletedEvent(CommentId commentId) : DomainEventBase
{
    public CommentId CommentId { get; } = commentId;
}
