using SocialMediaBackend.Modules.Users.Application.Abstractions;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;
using SocialMediaBackend.Modules.Users.Domain.Feed;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;

namespace SocialMediaBackend.Modules.Users.Application.Posts.LikePost;

public class LikePostCommandHandler(
    ApplicationDbContext context,
    IAuthorizationHandler<Post, PostId> authorizationHandler) : ICommandHandler<LikePostCommand>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAuthorizationHandler<Post, PostId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse> ExecuteAsync(LikePostCommand command, CancellationToken ct)
    {
        var post = await _context.Posts
            .Include(x => x.Author)
            .Include(x => x.Likes.Where(p => p.UserId == new AuthorId(command.UserId)))
            .Where(x => x.Id == command.PostId)
            .FirstOrDefaultAsync(ct);

        if (post is null) 
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);

        var authorized = await _authorizationHandler
            .AuthorizeAsync(new(command.UserId), command.PostId, new(command.IsAdmin), ct);

        if (!authorized)
            return ("The author limits who can view their posts", HandlerResponseStatus.Unauthorized, post.AuthorId);

        var like = post.AddLike(new(command.UserId));
        if (like is null)
            return ("User already liked this post", HandlerResponseStatus.Conflict, post.AuthorId);

        _context.Add(like);
        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.Created;
    }
}
