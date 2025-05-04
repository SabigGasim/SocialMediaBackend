using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Domain.Feed.Posts;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Posts.GetPost;

public class GetPostQueryHandler(
    ApplicationDbContext context,
    IAuthorizationHandler<Post, PostId> authService) : IQueryHandler<GetPostQuery, GetPostResponse>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAuthorizationHandler<Post, PostId> _authService = authService;

    public async Task<HandlerResponse<GetPostResponse>> ExecuteAsync(GetPostQuery query, CancellationToken ct)
    {
        var post = await _context.Posts
            .AsNoTracking()
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == query.PostId, ct);

        if (post is null)
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, query.PostId);

        var authorized = await _authService
            .AuthorizeAsync(new(query.UserId!.Value), query.PostId, new(query.IsAdmin), ct);

        if (!authorized)
            return ("The author limits who can view there posts", HandlerResponseStatus.Unauthorized, post.Id);


        return (post.MapToGetResponse(), HandlerResponseStatus.OK);
    }
}
