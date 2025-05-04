using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Common;
using SocialMediaBackend.Modules.Users.Domain.Feed;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Posts.UnlikePost;

public class UnlikePostCommandHandler(ApplicationDbContext context) : ICommandHandler<UnlikePostCommand>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(UnlikePostCommand command, CancellationToken ct)
    {
        var comment = await _context.Posts
            .Where(x => x.Id == command.PostId)
            .Include(x => x.Likes.Where(x => x.UserId == new AuthorId(command.UserId)))
            .FirstOrDefaultAsync(ct);

        if (comment is null)
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);

        var removed = comment.RemoveLike(new AuthorId(command.UserId));
        if (!removed)
            return ("User with the given Id didn't like this post", HandlerResponseStatus.Conflict, command.UserId);

        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.Deleted;
    }
}
