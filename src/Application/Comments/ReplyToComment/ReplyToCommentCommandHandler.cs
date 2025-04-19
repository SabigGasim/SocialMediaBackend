using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.ReplyToComment;

public class ReplyToCommentCommandHandler(ApplicationDbContext context)
    : ICommandHandler<ReplyToCommentCommand, ReplyToCommentResponse>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse<ReplyToCommentResponse>> ExecuteAsync(ReplyToCommentCommand command, CancellationToken ct)
    {
        var parent = await _context.Comments.FindAsync([command.ParentId], ct);
        if (parent is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound);

        var reply = parent.AddReply(parent.PostId, command.UserId, command.Text);

        _context.Add(reply);
        await _context.SaveChangesAsync(ct);

        return (reply.MapToReplyResponse(), HandlerResponseStatus.Created);
    }
}
