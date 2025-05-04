using SocialMediaBackend.Modules.Users.Domain.Common;

namespace SocialMediaBackend.Modules.Users.Domain.Feed.Comments;
public class CommentDeletedEvent(CommentId commentId) : DomainEventBase
{
    public CommentId CommentId { get; } = commentId;
}
