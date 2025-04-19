using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.EditComment;

public class EditCommentCommandHandler(ApplicationDbContext context) : ICommandHandler<EditCommentCommand>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(EditCommentCommand command, CancellationToken ct)
    {
        var comment = await _context.Comments.FindAsync(command.CommentId, ct);

        if(comment is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound, command.CommentId);

        comment.Edit(command.Text);

        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.NoContent;
    }
}
