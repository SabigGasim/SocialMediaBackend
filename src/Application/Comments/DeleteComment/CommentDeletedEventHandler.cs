using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.DeleteComment;

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
