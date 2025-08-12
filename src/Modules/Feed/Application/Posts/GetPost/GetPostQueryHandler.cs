using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Application.Mappings;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Posts.GetPost;

internal sealed class GetPostQueryHandler(
    FeedDbContext context,
    IAuthorizationHandler<Post, PostId> authorizationHandler,
    IAuthorContext authorContext) : IQueryHandler<GetPostQuery, GetPostResponse>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Post, PostId> _authHandler = authorizationHandler;
    private readonly IAuthorContext _authorContext = authorContext;

    public async Task<HandlerResponse<GetPostResponse>> ExecuteAsync(GetPostQuery query, CancellationToken ct)
    {
        var post = await _context.Posts
            .AsNoTracking()
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == query.PostId, ct);

        if (post is null)
        {
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, query.PostId);
        }

        if (!await _authHandler.AuthorizeAsync(_authorContext.AuthorId, query.PostId, ct))
        {
            return ("The author limits who can view there posts", HandlerResponseStatus.Unauthorized, post.Id);
        }

        return (post.MapToGetResponse(), HandlerResponseStatus.OK);
    }
}
