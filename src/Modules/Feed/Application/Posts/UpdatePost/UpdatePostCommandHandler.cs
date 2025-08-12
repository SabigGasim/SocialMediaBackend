using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.UpdatePost;

internal sealed class UpdatePostCommandHandler(
    FeedDbContext context,
    IAuthorizationHandler<Post, PostId> authorizationHandler,
    IAuthorContext authorContext) : ICommandHandler<UpdatePostCommand>
{
    public readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Post, PostId> _authorizationHandler = authorizationHandler;
    private readonly IAuthorContext _authorContext = authorContext;

    public async Task<HandlerResponse> ExecuteAsync(UpdatePostCommand command, CancellationToken ct)
    {
        var post = await _context.Posts.FindAsync([command.PostId], ct);
        if (post is null)
        {
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound);
        }

        if (!await _authorizationHandler.AuthorizeAsync(_authorContext.AuthorId, command.PostId, ct))
        {
            return ("Forbidden", HandlerResponseStatus.Unauthorized);
        }

        var result = post.UpdatePost(command.Text);

        return result.IsSuccess
            ? HandlerResponseStatus.Modified
            : result;
    }
}
