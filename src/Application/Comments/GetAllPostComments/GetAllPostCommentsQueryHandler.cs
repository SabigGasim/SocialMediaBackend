using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.GetAllPostComments;

public class GetAllPostCommentsQueryHandler(ApplicationDbContext context) 
    : IQueryHandler<GetAllPostCommentsQuery, GetAllPostCommentsResponse>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse<GetAllPostCommentsResponse>> ExecuteAsync(GetAllPostCommentsQuery query, CancellationToken ct)
    {
        var postExists = await _context.Posts.AnyAsync(x => x.Id == query.PostId, ct);
        if (!postExists)
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound);

        var sqlQuery = _context.Comments
            .AsNoTracking()
            .Where(c => c.ParentCommentId == null)
            .OrderByDescending(c => c.LikesCount)
            .OrderByDescending(c => c.Created);

        var totalCount = await sqlQuery.CountAsync(ct);

        var comments = await sqlQuery
            .Include(x => x.User)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync();

        return comments.MapToResponse(query.Page, query.PageSize, totalCount);
    }
}
