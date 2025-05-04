using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Abstractions;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Posts.UpdatePost;

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
