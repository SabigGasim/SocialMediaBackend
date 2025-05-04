using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.EditComment;

public class EditCommentCommandHandler(
    ApplicationDbContext context,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler) : ICommandHandler<EditCommentCommand>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAuthorizationHandler<Comment, CommentId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse> ExecuteAsync(EditCommentCommand command, CancellationToken ct)
    {
        var comment = await _context.Comments.FindAsync([command.CommentId], ct);

        if(comment is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound, command.CommentId);

        var authorized = await _authorizationHandler
            .IsAdminOrResourceOwnerAsync(new(command.UserId), command.CommentId, new(command.IsAdmin), ct);

        if (!authorized)
            return ("Forbidden", HandlerResponseStatus.Unauthorized);

        comment.Edit(command.Text);

        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.NoContent;
    }
}
