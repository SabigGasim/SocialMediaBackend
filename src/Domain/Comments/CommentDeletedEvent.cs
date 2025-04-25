using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Comments;
public class CommentDeletedEvent(CommentId commentId) : DomainEventBase
{
    public CommentId CommentId { get; } = commentId;
}
