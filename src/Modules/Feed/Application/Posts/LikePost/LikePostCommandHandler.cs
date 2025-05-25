using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.LikePost;

public class LikePostCommandHandler(
    FeedDbContext context,
    IAuthorizationHandler<Post, PostId> authorizationHandler) : ICommandHandler<LikePostCommand>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Post, PostId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse> ExecuteAsync(LikePostCommand command, CancellationToken ct)
    {
        var post = await _context.Posts
            .Include(x => x.Author)
            .Include(x => x.Likes.Where(p => p.UserId == new AuthorId(command.UserId)))
            .Where(x => x.Id == command.PostId)
            .FirstOrDefaultAsync(ct);

        if (post is null)
        {
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);
        }

        var authorId = new AuthorId(command.UserId);

        if (!await _authorizationHandler.AuthorizeAsync(authorId, command.PostId, new(command.IsAdmin), ct))
        {
            return ("The author limits who can view their posts", HandlerResponseStatus.Unauthorized, post.AuthorId);
        }

        var result = post.AddLike(new(command.UserId));
        if (!result.IsSuccess)
        {
            return result;
        }

        _context.Add(result.Payload);

        return HandlerResponseStatus.Created;
    }
}
