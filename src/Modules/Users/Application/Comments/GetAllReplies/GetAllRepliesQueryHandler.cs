using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Users.Application.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Auth;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Domain.Feed.Comments;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Comments.GetAllReplies;

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
            .AuthorizeAsync(new(query.UserId!.Value), query.ParentId, new(query.IsAdmin), ct);

        if (!authorized)
            return ("The author limits who can view their comments", HandlerResponseStatus.Unauthorized, query.ParentId);

        var queryable = _context.Comments
            .AsNoTracking()
            .Include(x => x.Author)
            .Where(x => x.ParentCommentId == query.ParentId);

        queryable = _authorizationHandler
            .AuthorizeQueryable(queryable, new(query.UserId!.Value), new AuthOptions(query.IsAdmin));

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
