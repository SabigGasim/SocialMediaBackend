using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SocialMediaBackend.Application.Posts.LikePost;

public class LikePostCommandHandler(
    ApplicationDbContext context,
    IAuthorizationHandler<Post, PostId> authorizationHandler) : ICommandHandler<LikePostCommand>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAuthorizationHandler<Post, PostId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse> ExecuteAsync(LikePostCommand command, CancellationToken ct)
    {
        var post = await _context.Posts
            .Include(x => x.User)
            .Include(x => x.Likes.Where(p => p.UserId == command.UserId))
            .Where(x => x.Id == command.PostId)
            .FirstOrDefaultAsync(ct);

        if (post is null) 
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);

        var authorized = await _authorizationHandler
            .AuthorizeAsync(command.UserId, command.PostId, new(command.IsAdmin), ct);

        if (!authorized)
            return ("The author limits who can view their posts", HandlerResponseStatus.Unauthorized, post.UserId);

        var like = post.AddLike(command.UserId);
        if (like is null)
            return ("User already liked this post", HandlerResponseStatus.Conflict, post.UserId);

        _context.Add(like);
        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.Created;
    }
}
