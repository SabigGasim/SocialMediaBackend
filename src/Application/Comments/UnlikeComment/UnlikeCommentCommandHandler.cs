using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.UnlikeComment;

public class UnlikeCommentCommandHandler(ApplicationDbContext context) : ICommandHandler<UnlikeCommentCommand>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(UnlikeCommentCommand command, CancellationToken ct)
    {
        var comment = await _context.Comments
            .Where(x => x.Id == command.CommentId)
            .Include(x => x.Likes.Where(x => x.UserId == command.UserId))
            .FirstOrDefaultAsync(ct);

        if (comment is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound, command.CommentId);

        var removed = comment.RemoveLike(command.UserId);
        if (!removed)
            return ("User with the given Id didn't like this comment", HandlerResponseStatus.Conflict, command.UserId);

        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.Deleted;
    }
}
