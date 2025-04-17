using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Posts.DeletePost;

public class DeletePostCommandHandler(ApplicationDbContext context) : ICommandHandler<DeletePostCommand>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(DeletePostCommand command, CancellationToken ct)
    {
        //TODO: Get UserId from Authentication

        var post = await _context.Posts
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == command.PostId, ct);

        if (post is null)
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);

        var user = post.User;

        var removed = user.RemovePost(command.PostId);
        if (!removed)
            return ("An internal error occurred", HandlerResponseStatus.InternalError);

        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.NoContent;
    }
}
