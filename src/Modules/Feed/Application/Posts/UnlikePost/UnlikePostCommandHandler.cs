using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.UnlikePost;

public class UnlikePostCommandHandler(FeedDbContext context) : ICommandHandler<UnlikePostCommand>
{
    private readonly FeedDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(UnlikePostCommand command, CancellationToken ct)
    {
        var comment = await _context.Posts
            .Where(x => x.Id == command.PostId)
            .Include(x => x.Likes.Where(x => x.UserId == new AuthorId(command.UserId)))
            .FirstOrDefaultAsync(ct);

        if (comment is null)
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);

        var removed = comment.RemoveLike(new AuthorId(command.UserId));
        if (!removed)
            return ("User with the given Id didn't like this post", HandlerResponseStatus.Conflict, command.UserId);

        return HandlerResponseStatus.Deleted;
    }
}
