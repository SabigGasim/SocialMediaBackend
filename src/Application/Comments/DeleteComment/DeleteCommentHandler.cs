using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.DeleteComment;

public class DeleteCommentHandler(ApplicationDbContext context) : ICommandHandler<DeleteCommentCommand>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(DeleteCommentCommand command, CancellationToken ct)
    {
        var post = await _context.Posts
            .Include(x => x.Comments.Where(c => c.Id == command.CommentId))
            .ThenInclude(x => x.ParentComment)
            .FirstOrDefaultAsync(x => x.Id == command.PostId);

        if (post is null)
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.CommentId);

        if (!post.Comments.Any())
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound, command.CommentId);

        post.RemoveComment(command.CommentId);

        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.NoContent;
    }
}
