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
    IAuthorizationHandler<Comment, CommentId> authorizationHandler)
    : IQueryHandler<GetCommentQuery, GetCommentResponse>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationHandler<Comment, CommentId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse<GetCommentResponse>> ExecuteAsync(GetCommentQuery query, CancellationToken ct)
    {
        var authorized = await _authorizationHandler
            .AuthorizeAsync(new AuthorId(query.UserId!.Value), query.CommentId, new AuthOptions(query.IsAdmin), ct);

        if (!authorized)
            return ("The author restricts who can see their comments", HandlerResponseStatus.Unauthorized, query.CommentId);

        var comment = await _context.Comments
            .AsNoTracking()
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == query.CommentId, ct);

        if (comment is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound);

        return (comment.MapToGetResponse(), HandlerResponseStatus.OK);
    }
}
