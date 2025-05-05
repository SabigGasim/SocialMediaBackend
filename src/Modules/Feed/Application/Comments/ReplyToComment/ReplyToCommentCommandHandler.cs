using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Application.Mappings;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.ReplyToComment;

public class ReplyToCommentCommandHandler(
    FeedDbContext context,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler)
    : ICommandHandler<ReplyToCommentCommand, ReplyToCommentResponse>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Comment, CommentId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse<ReplyToCommentResponse>> ExecuteAsync(ReplyToCommentCommand command, CancellationToken ct)
    {
        var parent = await _context.Comments.FindAsync([command.ParentId], ct);
        if (parent is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound);

        var authorized = await _authorizationHandler
            .AuthorizeAsync(new(command.UserId), command.ParentId, new(command.IsAdmin), ct);

        if (!authorized)
            return ("The author restricts who can see their comments", HandlerResponseStatus.Unauthorized, parent.AuthorId);

        var reply = parent.AddReply(new(command.UserId), command.Text);

        _context.Add(reply);
        await _context.SaveChangesAsync(ct);

        return (reply.MapToReplyResponse(), HandlerResponseStatus.Created);
    }
}
