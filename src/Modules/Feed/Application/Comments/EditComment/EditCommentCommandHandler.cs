using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.EditComment;

internal sealed class EditCommentCommandHandler(
    FeedDbContext context,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler,
    IAuthorContext authorContext) : ICommandHandler<EditCommentCommand>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Comment, CommentId> _authHandler = authorizationHandler;
    private readonly IAuthorContext _authorContext = authorContext;

    public async Task<HandlerResponse> ExecuteAsync(EditCommentCommand command, CancellationToken ct)
    {
        var comment = await _context.Comments.FindAsync([command.CommentId], ct);
        if (comment is null)
        {
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound, command.CommentId);
        }

        if (!await _authHandler.IsAdminOrResourceOwnerAsync(_authorContext.AuthorId, command.CommentId, ct))
        {
            return ("Forbidden", HandlerResponseStatus.Unauthorized);
        }

        comment.Edit(command.Text);

        return HandlerResponseStatus.NoContent;
    }
}
