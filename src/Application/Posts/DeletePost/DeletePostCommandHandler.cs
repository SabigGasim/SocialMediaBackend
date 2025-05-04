using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Domain.Feed.Posts;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Posts.DeletePost;

public class DeletePostCommandHandler(
    ApplicationDbContext context,
    IAuthorizationHandler<Post, PostId> authorizationHandler) : ICommandHandler<DeletePostCommand>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAuthorizationHandler<Post, PostId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse> ExecuteAsync(DeletePostCommand command, CancellationToken ct)
    {   
        var authorized = await _authorizationHandler
            .IsAdminOrResourceOwnerAsync(command.UserId, command.PostId, new(command.IsAdmin), ct);

        if (!authorized)
        {
            return ("Forbidden", HandlerResponseStatus.Unauthorized);
        }

        var query = _context.Users
                .Include(x => x.Posts.Where(p => p.Id == command.PostId))
                .AsQueryable();

        query = command.IsAdmin
            ? query.Where(x => x.Id == command.UserId || x.Posts.Any(p => p.Id == command.PostId))
            : query.Where(x => x.Id == command.UserId);

        var user = await query.FirstOrDefaultAsync(ct);
        if (user is null)
        {
            // This will only execute if the deleter is not the post owner
            // rather, an admin. If the admin provided an Invalid PostId,
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

        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.NoContent;
    }
}
