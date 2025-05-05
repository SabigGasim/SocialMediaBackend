using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Feed.Domain.Comments;
public class CommentDeletedEvent(CommentId commentId) : DomainEventBase
{
    public CommentId CommentId { get; } = commentId;
}
