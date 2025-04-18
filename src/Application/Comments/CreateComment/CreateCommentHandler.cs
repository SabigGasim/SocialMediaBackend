using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.CreateComment;

public class CreateCommentHandler(ApplicationDbContext context)
    : ICommandHandler<CreateCommentCommand, CreateCommentResponse>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse<CreateCommentResponse>> ExecuteAsync(CreateCommentCommand command, CancellationToken ct)
    {
        var userExists = await _context.Users
            .AsNoTracking()
            .AnyAsync(x => x.Id == command.UserId, ct);

        if (!userExists)
            return ("Invalid user Id", HandlerResponseStatus.BadRequest);

        var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == command.PostId, ct);

        if (post is null)
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);

        var comment = post.AddComment(command.Text, command.UserId);

        if (comment is null)
            return ("An internal error occurred", HandlerResponseStatus.InternalError);

        _context.Add(comment);
        await _context.SaveChangesAsync(ct);

        return (comment.MapToCreateResponse(), HandlerResponseStatus.Created);
    }
}
