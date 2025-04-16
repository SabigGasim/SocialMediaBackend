using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Posts.GetPost;

public class GetPostQueryHandler(ApplicationDbContext context) : IQueryHandler<GetPostQuery, GetPostResponse>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<HandlerResponse<GetPostResponse>> ExecuteAsync(GetPostQuery query, CancellationToken ct)
    {
        var post = await _context.Posts
            .AsNoTracking()
            .Where(x => x.Id == query.PostId)
            .Include(x => x.User)
            .FirstOrDefaultAsync(ct);

        return post is not null
            ? (post.MapToGetResponse(), HandlerResponseStatus.OK)
            : ("Post with the given Id was not found", HandlerResponseStatus.NotFound, query.PostId);
    }
}
