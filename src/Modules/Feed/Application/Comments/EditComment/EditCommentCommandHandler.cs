using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.EditComment;

public class EditCommentCommandHandler(
    FeedDbContext context,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler) : ICommandHandler<EditCommentCommand>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Comment, CommentId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse> ExecuteAsync(EditCommentCommand command, CancellationToken ct)
    {
        var comment = await _context.Comments.FindAsync([command.CommentId], ct);

        if (comment is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound, command.CommentId);

        var authorized = await _authorizationHandler
            .IsAdminOrResourceOwnerAsync(new(command.UserId), command.CommentId, new(command.IsAdmin), ct);

        if (!authorized)
            return ("Forbidden", HandlerResponseStatus.Unauthorized);

        comment.Edit(command.Text);

        return HandlerResponseStatus.NoContent;
    }
}
