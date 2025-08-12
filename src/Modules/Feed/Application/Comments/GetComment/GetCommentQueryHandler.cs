using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Queries;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Application.Mappings;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Comments;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.GetComment;

internal sealed class GetCommentQueryHandler(
    FeedDbContext context,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler,
    IAuthorContext authorContext)
    : IQueryHandler<GetCommentQuery, GetCommentResponse>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Comment, CommentId> _authHandler = authorizationHandler;
    private readonly IAuthorContext _authorContext = authorContext;

    public async Task<HandlerResponse<GetCommentResponse>> ExecuteAsync(GetCommentQuery query, CancellationToken ct)
    {
        if (!await _authHandler.AuthorizeAsync(_authorContext.AuthorId, query.CommentId, ct))
        {
            return ("This author restricts who can see their comments", HandlerResponseStatus.Unauthorized, query.CommentId.Value);
        }

        var comment = await _context.Comments
            .AsNoTracking()
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == query.CommentId, ct);

        if (comment is null)
        {
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound);
        }

        return (comment.MapToGetResponse(), HandlerResponseStatus.OK);
    }
}
