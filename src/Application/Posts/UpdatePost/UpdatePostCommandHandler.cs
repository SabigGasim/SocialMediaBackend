using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Posts.UpdatePost;

public class UpdatePostCommandHandler(ApplicationDbContext context) : ICommandHandler<UpdatePostCommand>
{
    public readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse> ExecuteAsync(UpdatePostCommand command, CancellationToken ct)
    {
        var post = await _context.Posts.FindAsync(command.PostId);
        if(post is null)
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound);

        var updated = post.UpdatePost(command.Text);
        if(updated)
            await _context.SaveChangesAsync(ct);

        return updated
            ? HandlerResponseStatus.Modified
            : ("An internal error occured", HandlerResponseStatus.InternalError);
    }
}
