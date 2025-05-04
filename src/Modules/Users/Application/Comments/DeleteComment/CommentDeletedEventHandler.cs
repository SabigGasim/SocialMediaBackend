using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Users.Domain.Feed.Comments;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Comments.DeleteComment;

public class CommentDeletedEventHandler(ApplicationDbContext context) : IDomainEventHandler<CommentDeletedEvent>
{
    private readonly ApplicationDbContext _context = context;

    public async ValueTask Handle(CommentDeletedEvent notification, CancellationToken cancellationToken)
    {
        var parent = await _context.Comments
            .Where(x => x.Id == notification.CommentId && x.ParentCommentId != null)
            .Select(x => x.ParentComment)
            .FirstOrDefaultAsync(cancellationToken);

        parent?.RemoveReply(notification.CommentId);
    }
}
