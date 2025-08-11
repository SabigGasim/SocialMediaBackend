using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.UnlikeComment;

internal sealed class UnlikeCommentCommandHandler(
    FeedDbContext context,
    IAuthorContext authorContext) : ICommandHandler<UnlikeCommentCommand>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorContext _authorContext = authorContext;

    public async Task<HandlerResponse> ExecuteAsync(UnlikeCommentCommand command, CancellationToken ct)
    {
        var comment = await _context.Comments
            .Where(x => x.Id == command.CommentId)
            .Include(x => x.Likes.Where(x => x.UserId == _authorContext.AuthorId))
            .FirstOrDefaultAsync(ct);

        if (comment is null)
        {
            return ("Comment was not found", HandlerResponseStatus.NotFound, command.CommentId);
        }

        var result = comment.RemoveLike(_authorContext.AuthorId);

        return result.IsSuccess
            ? HandlerResponseStatus.Deleted
            : result;
    }
}
