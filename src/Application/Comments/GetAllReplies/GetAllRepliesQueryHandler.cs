using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.GetAllReplies;

public class GetAllRepliesQueryHandler(
    ApplicationDbContext context,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler)
    : IQueryHandler<GetAllRepliesQuery, GetAllRepliesResponse>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAuthorizationHandler<Comment, CommentId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse<GetAllRepliesResponse>> ExecuteAsync(GetAllRepliesQuery query, CancellationToken ct)
    {
        var comment = await _context.Comments.FindAsync([query.ParentId], ct);

        if (comment is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound);

        var authorized = await _authorizationHandler
            .AuthorizeAsync(query.UserId, query.ParentId, new(query.IsAdmin), ct);

        if (!authorized)
            return ("The author limits who can view their comments", HandlerResponseStatus.Unauthorized, query.ParentId);

        var queryable = _context.Comments
            .AsNoTracking()
            .Include(x => x.User)
            .Where(x => x.ParentCommentId == query.ParentId);

        queryable = _authorizationHandler
            .AuthorizeQueryable(queryable, query.UserId, new(query.IsAdmin));

        var replies = await queryable
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        var count = await queryable.CountAsync(ct);

        var response = replies
            .MapToGetRepliesResponse(query.ParentId, query.Page, query.PageSize, count);

        return (response, HandlerResponseStatus.OK);
    }
}
