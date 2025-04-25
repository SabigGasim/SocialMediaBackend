using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.GetComment;

public class GetCommentQueryHandler(
    ApplicationDbContext context,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler)
    : IQueryHandler<GetCommentQuery, GetCommentResponse>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAuthorizationHandler<Comment, CommentId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse<GetCommentResponse>> ExecuteAsync(GetCommentQuery query, CancellationToken ct)
    {
        var authorized = await _authorizationHandler
            .AuthorizeAsync(query.UserId, query.CommentId, new(query.IsAdmin), ct);

        if (!authorized)
            return ("The author restricts who can see their comments", HandlerResponseStatus.Unauthorized, query.CommentId);

        var comment = await _context.Comments
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == query.CommentId, ct);

        if (comment is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound);

        return (comment.MapToGetResponse(), HandlerResponseStatus.OK);
    }
}
