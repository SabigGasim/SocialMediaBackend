using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Services;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.DomainServices.Comments;

public class CommentLookupService(ApplicationDbContext context) : ICommentLookupService
{
    private readonly ApplicationDbContext _context = context;

    public Task<Comment?> FindAsync(
        Guid commentId,
        bool includeLikes = false,
        bool includeParent = false,
        CancellationToken ct = default)
    {
        var comments = _context.Comments.Where(x => x.Id == commentId);

        comments = includeLikes
            ? comments.Include(x => x.Likes)
            : comments;

        comments = includeParent
            ? comments.Include(x => x.ParentComment)
            : comments;

        return comments.FirstOrDefaultAsync(ct);
    }

    public Task<Comment?> FindCommentLikedByUser(
        Guid commentId,
        Guid userId,
        bool includeParent = false,
        CancellationToken ct = default)
    {
        var comments = _context.Comments
            .Where(x => x.Id == commentId)
            .Include(x => x.Likes
                .Where(x => x.UserId == userId)
                .Take(1));

        return includeParent
            ? comments.Include(x => x.ParentComment).FirstOrDefaultAsync(ct)
            : comments.FirstOrDefaultAsync(ct);
    }

    public Task<bool> ExistsAsync(Guid commentId, CancellationToken ct = default)
    {
        return _context.Comments.AnyAsync(x => x.Id == commentId, ct);
    }
}




