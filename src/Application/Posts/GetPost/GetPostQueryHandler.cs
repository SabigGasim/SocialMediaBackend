using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Application.Auth;
using SocialMediaBackend.Application.Auth.Posts;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Posts.GetPost;

public class GetPostQueryHandler(
    ApplicationDbContext context,
    IAuthorizationService<Post, PostId> authService) : IQueryHandler<GetPostQuery, GetPostResponse>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAuthorizationService<Post, PostId> _authService = authService;

    public async Task<HandlerResponse<GetPostResponse>> ExecuteAsync(GetPostQuery query, CancellationToken ct)
    {
        var authorized = await _authService
            .AuthorizeAsync(query.UserId, query.PostId, new(query.IsAdmin), ct);

        if (!authorized)
        {
            return ("The author limits who can view this post", HandlerResponseStatus.Unauthorized, query.PostId);
        }

        var post = await _context.Posts
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == query.PostId, ct);

        return post is not null
            ? (post.MapToGetResponse(), HandlerResponseStatus.OK)
            : ("Post with the given Id was not found", HandlerResponseStatus.NotFound, query.PostId);
    }
}
