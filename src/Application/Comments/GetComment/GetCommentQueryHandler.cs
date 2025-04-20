using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.GetComment;

public class GetCommentQueryHandler(ApplicationDbContext context)
    : IQueryHandler<GetCommentQuery, GetCommentResponse>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse<GetCommentResponse>> ExecuteAsync(GetCommentQuery query, CancellationToken ct)
    {
        var comment = await _context.Comments
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == query.CommentId, ct);

        if (comment is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound);

        return (comment.MapToGetResponse(), HandlerResponseStatus.OK);
    }
}
