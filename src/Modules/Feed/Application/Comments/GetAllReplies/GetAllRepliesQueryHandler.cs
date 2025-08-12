using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Application.Mappings;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.GetAllReplies;

internal sealed class GetAllRepliesQueryHandler(
    FeedDbContext context,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler,
    IAuthorContext authorContext)
    : IQueryHandler<GetAllRepliesQuery, GetAllRepliesResponse>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Comment, CommentId> _authorizationHandler = authorizationHandler;
    private readonly IAuthorContext _authorContext = authorContext;

    public async Task<HandlerResponse<GetAllRepliesResponse>> ExecuteAsync(GetAllRepliesQuery query, CancellationToken ct)
    {
        var comment = await _context.Comments.FindAsync([query.ParentId], ct);
        if (comment is null)
        {
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound);
        }

        if (!await _authorizationHandler.AuthorizeAsync(_authorContext.AuthorId, query.ParentId, ct))
        {
            return ("The author limits who can view their comments", HandlerResponseStatus.Unauthorized, query.ParentId);
        }

        var queryable = _context.Comments
            .AsNoTracking()
            .Include(x => x.Author)
            .Where(x => x.ParentCommentId == query.ParentId);

        queryable = await _authorizationHandler.AuthorizeQueryable(queryable, _authorContext.AuthorId);

        var replies = await queryable
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        var count = await queryable.CountAsync(ct);
        var response = replies.MapToGetRepliesResponse(query.ParentId, query.Page, query.PageSize, count);

        return (response, HandlerResponseStatus.OK);
    }
}
