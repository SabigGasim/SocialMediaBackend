using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.LikePost;

internal sealed class LikePostCommandHandler(
    FeedDbContext context,
    IAuthorizationHandler<Post, PostId> authorizationHandler,
    IAuthorContext authorContext) : ICommandHandler<LikePostCommand>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Post, PostId> _authorizationHandler = authorizationHandler;
    private readonly IAuthorContext _authorContext = authorContext;

    public async Task<HandlerResponse> ExecuteAsync(LikePostCommand command, CancellationToken ct)
    {
        var authorId = _authorContext.AuthorId;

        var post = await _context.Posts
            .Include(x => x.Author)
            .Include(x => x.Likes.Where(p => p.UserId == authorId))
            .Where(x => x.Id == command.PostId)
            .FirstOrDefaultAsync(ct);

        if (post is null)
        {
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);
        }

        if (!await _authorizationHandler.AuthorizeAsync(authorId, command.PostId, ct))
        {
            return ("The author limits who can view their posts", HandlerResponseStatus.Unauthorized, post.AuthorId);
        }

        var result = post.AddLike(authorId);
        if (!result.IsSuccess)
        {
            return result;
        }

        _context.Add(result.Payload);

        return HandlerResponseStatus.Created;
    }
}
