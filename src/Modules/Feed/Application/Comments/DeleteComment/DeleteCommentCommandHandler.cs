using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.DeleteComment;

internal sealed class DeleteCommentCommandHandler(
    FeedDbContext context,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler,
    IAuthorContext authorContext) : ICommandHandler<DeleteCommentCommand>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Comment, CommentId> _authorizationHandler = authorizationHandler;
    private readonly IAuthorContext _authorContext = authorContext;

    public async Task<HandlerResponse> ExecuteAsync(DeleteCommentCommand command, CancellationToken ct)
    {
        var authorId = _authorContext.AuthorId;
        var commentId = command.CommentId;
        var postId = command.PostId;

        if (!await _authorizationHandler.IsAdminOrResourceOwnerAsync(authorId, commentId, new(true), ct))
        {
            return ("Forbidden", HandlerResponseStatus.Unauthorized);
        }

        var post = await _context.Posts
            .Include(x => x.Comments.Where(p => p.Id == commentId))
            .Where(x => x.Id == postId)
            .FirstOrDefaultAsync(ct);

        if (post is null)
        {
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, postId);
        }

        var result = post.RemoveComment(commentId);

        return result.IsSuccess
            ? HandlerResponseStatus.NoContent
            : result;
    }
}
