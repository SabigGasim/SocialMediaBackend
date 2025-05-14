using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.UnlikeComment;

public class UnlikeCommentCommandHandler(FeedDbContext context) : ICommandHandler<UnlikeCommentCommand>
{
    private readonly FeedDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(UnlikeCommentCommand command, CancellationToken ct)
    {
        var comment = await _context.Comments
            .Where(x => x.Id == command.CommentId)
            .Include(x => x.Likes.Where(x => x.UserId == new AuthorId(command.UserId)))
            .FirstOrDefaultAsync(ct);

        if (comment is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound, command.CommentId);

        var removed = comment.RemoveLike(new AuthorId(command.UserId));
        if (!removed)
            return ("User with the given Id didn't like this comment", HandlerResponseStatus.Conflict, command.UserId);

        return HandlerResponseStatus.Deleted;
    }
}
