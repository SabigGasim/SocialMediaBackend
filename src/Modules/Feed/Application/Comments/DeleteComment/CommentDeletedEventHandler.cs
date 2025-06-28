using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.DeleteComment;

internal sealed class CommentDeletedEventHandler(FeedDbContext context) : IDomainEventHandler<CommentDeletedEvent>
{
    private readonly FeedDbContext _context = context;

    public async ValueTask Handle(CommentDeletedEvent notification, CancellationToken cancellationToken)
    {
        var parent = await _context.Comments
            .Where(x => x.Id == notification.CommentId && x.ParentCommentId != null)
            .Select(x => x.ParentComment)
            .FirstOrDefaultAsync(cancellationToken);

        parent?.RemoveReply(notification.CommentId);
    }
}
