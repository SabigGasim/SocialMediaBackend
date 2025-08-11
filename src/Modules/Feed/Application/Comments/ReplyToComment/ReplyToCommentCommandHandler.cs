using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Application.Mappings;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.ReplyToComment;

internal sealed class ReplyToCommentCommandHandler(
    FeedDbContext context,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler,
    IAuthorContext authorContext)
    : ICommandHandler<ReplyToCommentCommand, ReplyToCommentResponse>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Comment, CommentId> _authorizationHandler = authorizationHandler;
    private readonly IAuthorContext _authorContext = authorContext;

    public async Task<HandlerResponse<ReplyToCommentResponse>> ExecuteAsync(ReplyToCommentCommand command, CancellationToken ct)
    {
        var parent = await _context.Comments.FindAsync([command.ParentId], ct);
        if (parent is null)
        {
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound);
        }

        var authorized = await _authorizationHandler.AuthorizeAsync(
            _authorContext.AuthorId, 
            command.ParentId, 
            new AuthOptions(IsAdmin: true), 
            ct);

        if (!authorized)
        {
            return ("The author restricts who can see their comments", HandlerResponseStatus.Unauthorized, parent.AuthorId);
        }

        var reply = parent.AddReply(_authorContext.AuthorId, command.Text);
        _context.Add(reply);

        return (reply.MapToReplyResponse(), HandlerResponseStatus.Created);
    }
}
