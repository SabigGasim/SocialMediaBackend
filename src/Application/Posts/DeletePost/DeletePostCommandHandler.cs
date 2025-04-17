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
        var rowsAffected = await _context.Posts
            .Where(x => x.Id == command.PostId)
            .ExecuteDeleteAsync(ct);

        var deleted = rowsAffected > 0;

        return deleted
            ? HandlerResponseStatus.NoContent
            : ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);
    }
}
