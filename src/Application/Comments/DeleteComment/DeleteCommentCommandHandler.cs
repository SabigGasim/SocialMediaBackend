using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.DeleteComment;

public class DeleteCommentCommandHandler(
    ApplicationDbContext context,
    IUnitOfWork unitOfWork,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler) : ICommandHandler<DeleteCommentCommand>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAuthorizationHandler<Comment, CommentId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse> ExecuteAsync(DeleteCommentCommand command, CancellationToken ct)
    {
        var authorized = await _authorizationHandler
            .IsAdminOrResourceOwnerAsync(command.UserId, command.CommentId, new(command.IsAdmin), ct);

        if (!authorized)
        {
            return ("Forbidden", HandlerResponseStatus.Unauthorized);
        }

        var query = _context.Posts
                .Include(x => x.Comments.Where(p => p.Id == command.CommentId))
                .Where(x => x.Id == command.PostId)
                .AsQueryable();

        var post = await query.FirstOrDefaultAsync(ct);
        if (post is null)
        {
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);
        }

        var removed = post.RemoveComment(command.CommentId);
        if (!removed)
        {
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound, command.CommentId);
        }

        await _unitOfWork.CommitAsync(ct);

        return HandlerResponseStatus.NoContent;
    }
}
