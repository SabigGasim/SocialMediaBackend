using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Domain.Feed.Comments;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Comments.ReplyToComment;

public class ReplyToCommentCommandHandler(
    ApplicationDbContext context,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler)
    : ICommandHandler<ReplyToCommentCommand, ReplyToCommentResponse>
{
    private readonly ApplicationDbContext _context = context;
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
