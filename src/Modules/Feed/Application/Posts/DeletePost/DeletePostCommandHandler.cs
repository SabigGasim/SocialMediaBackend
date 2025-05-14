using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.DeletePost;

public class DeletePostCommandHandler(
    FeedDbContext context,
    IAuthorizationHandler<Post, PostId> authorizationHandler) : ICommandHandler<DeletePostCommand>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Post, PostId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse> ExecuteAsync(DeletePostCommand command, CancellationToken ct)
    {
        var authorized = await _authorizationHandler
            .IsAdminOrResourceOwnerAsync(new AuthorId(command.UserId), command.PostId, new AuthOptions(command.IsAdmin), ct);

        if (!authorized)
        {
            return ("Forbidden", HandlerResponseStatus.Unauthorized);
        }

        var query = _context.Authors
                .Include(x => x.Posts.Where(p => p.Id == command.PostId))
                .AsQueryable();

        query = command.IsAdmin
            ? query.Where(x => x.Id == new AuthorId(command.UserId) || x.Posts.Any(p => p.Id == command.PostId))
            : query.Where(x => x.Id == new AuthorId(command.UserId));

        var user = await query.FirstOrDefaultAsync(ct);
        if (user is null)
        {
            // This will only execute if the deleter is not the post owner
            // AND an admin. If the admin provided an Invalid PostId,
            // The query will not return the user associated with it
            // as it runs backwards.
            // Horrible sql, but the P in EF Core stands for "Performance",
            // so should we even care? Maybe...
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);
        }

        var removed = user.RemovePost(command.PostId);
        if (!removed)
        {
            return ("Post with the given Id was not found or is not associated with the given user", HandlerResponseStatus.NotFound, command.PostId);
        }

        return HandlerResponseStatus.NoContent;
    }
}
