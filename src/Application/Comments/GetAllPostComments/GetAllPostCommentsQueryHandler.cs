using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Auth;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Domain.Feed.Comments;
using SocialMediaBackend.Domain.Feed.Posts;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.GetAllPostComments;

public class GetAllPostCommentsQueryHandler(
    ApplicationDbContext context,
    IAuthorizationService authorizationService
    ) 
    : IQueryHandler<GetAllPostCommentsQuery, GetAllPostCommentsResponse>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    public async Task<HandlerResponse<GetAllPostCommentsResponse>> ExecuteAsync(GetAllPostCommentsQuery query, CancellationToken ct)
    {
        var postExists = await _context.Posts.AnyAsync(x => x.Id == query.PostId, ct);
        if (!postExists)
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound);

        var authorized = await _authorizationService
            .AuthorizeAsync<Post, PostId>(query.UserId, query.PostId, new(query.IsAdmin), ct);

        if (!authorized)
            return ("The author limits who can view there posts", HandlerResponseStatus.Unauthorized, query.PostId);

        var sqlQuery = _context.Comments
            .AsNoTracking()
            .Where(c => c.ParentCommentId == null)
            .OrderByDescending(c => c.LikesCount)
            .OrderByDescending(c => c.Created)
            .AsQueryable();

        sqlQuery = _authorizationService
            .AuthorizeQueryable<Comment, CommentId>(sqlQuery, query.UserId, new(query.IsAdmin));

        var totalCount = await sqlQuery.CountAsync(ct);

        var comments = await sqlQuery
            .Include(x => x.User)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return comments.MapToResponse(query.Page, query.PageSize, totalCount);
    }
}
