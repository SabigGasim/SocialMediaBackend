using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.UnlikePost;

internal sealed class UnlikePostCommandHandler(
    FeedDbContext context,
    IAuthorContext authorContext) : ICommandHandler<UnlikePostCommand>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorContext _authorContext = authorContext;

    public async Task<HandlerResponse> ExecuteAsync(UnlikePostCommand command, CancellationToken ct)
    {
        var comment = await _context.Posts
            .Where(x => x.Id == command.PostId)
            .Include(x => x.Likes.Where(x => x.UserId == _authorContext.AuthorId))
            .FirstOrDefaultAsync(ct);

        if (comment is null)
        {
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);
        }

        var result = comment.RemoveLike(_authorContext.AuthorId);

        return result.IsSuccess 
            ? HandlerResponseStatus.Deleted
            : result;
    }
}
