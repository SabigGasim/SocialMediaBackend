using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.DeleteComment;

public class DeleteCommentCommandHandler(
    FeedDbContext context,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler) : ICommandHandler<DeleteCommentCommand>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Comment, CommentId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse> ExecuteAsync(DeleteCommentCommand command, CancellationToken ct)
    {
        var authorized = await _authorizationHandler
            .IsAdminOrResourceOwnerAsync(new AuthorId(command.UserId), command.CommentId, new AuthOptions(command.IsAdmin), ct);

        if (!authorized)
        {
            return ("Forbidden", HandlerResponseStatus.Unauthorized);
        }

        var query = _context.Posts
                .Include(x => x.Comments.Where(p => p.Id == command.CommentId))
                .Where(x => x.Id == command.PostId)
                .AsQueryable();

        var post = await query.FirstOrDefaultAsync(ct);
        if (post is null)
        {
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);
        }

        var removed = post.RemoveComment(command.CommentId);
        if (!removed)
        {
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound, command.CommentId);
        }

        return HandlerResponseStatus.NoContent;
    }
}
