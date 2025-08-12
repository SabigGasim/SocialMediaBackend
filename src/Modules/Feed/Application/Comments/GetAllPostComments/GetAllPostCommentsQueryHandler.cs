using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Application.Mappings;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.GetAllPostComments;

internal sealed class GetAllPostCommentsQueryHandler(
    FeedDbContext context,
    IAuthorizationService authorizationService,
    IAuthorContext authorContext)
    : IQueryHandler<GetAllPostCommentsQuery, GetAllPostCommentsResponse>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationService _authService = authorizationService;
    private readonly IAuthorContext _authorContext = authorContext;

    public async Task<HandlerResponse<GetAllPostCommentsResponse>> ExecuteAsync(GetAllPostCommentsQuery query, CancellationToken ct)
    {
        var postExists = await _context.Posts.AnyAsync(x => x.Id == query.PostId, ct);
        if (!postExists)
        {
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound);
        }

        if (!await _authService.AuthorizeAsync<Post, PostId>(_authorContext.AuthorId, query.PostId, ct))
        {
            return ("The author limits who can view there posts", HandlerResponseStatus.Unauthorized, query.PostId);
        }

        var sqlQuery = _context.Comments
            .AsNoTracking()
            .Where(c => c.ParentCommentId == null)
            .OrderByDescending(c => c.LikesCount)
            .OrderByDescending(c => c.Created)
            .AsQueryable();

        sqlQuery = await _authService.AuthorizeQueryable<Comment, CommentId>(sqlQuery, _authorContext.AuthorId, ct);

        var totalCount = await sqlQuery.CountAsync(ct);

        var comments = await sqlQuery
            .Include(x => x.Author)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        return comments.MapToResponse(query.Page, query.PageSize, totalCount);
    }
}
