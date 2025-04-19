using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.GetAllReplies;

public class GetAllRepliesQueryHandler(ApplicationDbContext context)
    : IQueryHandler<GetAllRepliesQuery, GetAllRepliesResponse>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse<GetAllRepliesResponse>> ExecuteAsync(GetAllRepliesQuery query, CancellationToken ct)
    {
        var comment = await _context.Comments.FindAsync([query.ParentId], ct);

        if (comment is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound);

        var replies = await _context.Comments
            .AsNoTracking()
            .Include(x => x.User)
            .Where(x => x.ParentCommentId == query.ParentId)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(ct);

        var response = replies
            .MapToGetRepliesResponse(query.ParentId, query.Page, query.PageSize, comment.RepliesCount);

        return (response, HandlerResponseStatus.OK);
    }
}
