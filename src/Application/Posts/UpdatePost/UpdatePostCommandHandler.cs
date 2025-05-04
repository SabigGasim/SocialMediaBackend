using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Domain.Feed.Posts;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Posts.UpdatePost;

public class UpdatePostCommandHandler(
    ApplicationDbContext context,
    IAuthorizationHandler<Post, PostId> authorizationHandler) : ICommandHandler<UpdatePostCommand>
{
    public readonly ApplicationDbContext _context = context;
    private readonly IAuthorizationHandler<Post, PostId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse> ExecuteAsync(UpdatePostCommand command, CancellationToken ct)
    {
        var post = await _context.Posts.FindAsync([command.PostId], ct);
        if (post is null)
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound);

        var authorized = await _authorizationHandler
            .AuthorizeAsync(new(command.UserId), command.PostId, new(command.IsAdmin), ct);

        if (!authorized)
            return ("Forbidden", HandlerResponseStatus.Unauthorized);

        post.UpdatePost(command.Text);

        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.Modified;
    }
}
