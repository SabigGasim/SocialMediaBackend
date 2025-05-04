using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Users.Application.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests;
using SocialMediaBackend.Modules.Users.Application.Abstractions.Requests.Queries;
using SocialMediaBackend.Modules.Users.Application.Common;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Domain.Feed.Comments;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Comments.GetComment;

public class GetCommentQueryHandler(
    ApplicationDbContext context,
    IAuthorizationHandler<Comment, CommentId> authorizationHandler)
    : IQueryHandler<GetCommentQuery, GetCommentResponse>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAuthorizationHandler<Comment, CommentId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse<GetCommentResponse>> ExecuteAsync(GetCommentQuery query, CancellationToken ct)
    {
        var authorized = await _authorizationHandler
            .AuthorizeAsync(new(query.UserId!.Value), query.CommentId, new(query.IsAdmin), ct);

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
