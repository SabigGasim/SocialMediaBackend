using SocialMediaBackend.Modules.Users.Application.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Common;
using SocialMediaBackend.Modules.Users.Domain.Feed.Comments;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Comments.EditComment;

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
