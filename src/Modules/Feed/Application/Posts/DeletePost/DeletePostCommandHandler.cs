using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.DeletePost;

internal sealed class DeletePostCommandHandler(
    FeedDbContext context,
    IAuthorizationHandler<Post, PostId> authorizationHandler,
    IAuthorContext authorContext) : ICommandHandler<DeletePostCommand>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Post, PostId> _authorizationHandler = authorizationHandler;
    private readonly IAuthorContext _authorContext = authorContext;

    public async Task<HandlerResponse> ExecuteAsync(DeletePostCommand command, CancellationToken ct)
    {
        var authResult = await _authorizationHandler.IsAdminOrResourceOwnerAsync(
            _authorContext.AuthorId, 
            command.PostId,
            ct);

        if (!authResult.IsAuthorized)
        {
            return ("Forbidden", HandlerResponseStatus.Unauthorized);
        }

        //TODO: Return result from auth handler that indicates how the authorization succeded, including
        //if the user was admin
        var query = authResult.SuccessReason == AuthSuccessReason.Admin
            ? _context
                .Authors
                .Where(x => 
                    x.Id == _authorContext.AuthorId ||
                    x.Posts.Any(p => p.Id == command.PostId))
            : _context
                .Authors
                .Where(x => x.Id == _authorContext.AuthorId);

        var user = await query
            .Include(x => x.Posts.Where(p => p.Id == command.PostId))
            .FirstOrDefaultAsync(ct);

        if (user is null)
        {
            // This will only execute if the deleter is an admin That isn't
            // the resource owner, and has provided an invalid PostId.
            // (command.IsAdmin && Post.AuthorId != authorId && PostId is doesn't exist)
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);
        }

        var result = user.RemovePost(command.PostId);

        return result.IsSuccess
            ? HandlerResponseStatus.NoContent
            : result;
    }
}
