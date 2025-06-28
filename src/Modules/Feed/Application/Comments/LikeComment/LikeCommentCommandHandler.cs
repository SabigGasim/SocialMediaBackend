using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Domain.Comments;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.LikeComment;
internal sealed class LikeCommentCommandHandler(
    FeedDbContext context,
    IAuthorizationService authorizationService) : ICommandHandler<LikeCommentCommand>
{
    private readonly FeedDbContext _context = context;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    public async Task<HandlerResponse> ExecuteAsync(LikeCommentCommand command, CancellationToken ct)
    {
        var comment = await _context.Comments
            .Include(x => x.Author)
            .Include(x => x.Likes.Where(p => p.UserId == new AuthorId(command.UserId)))
            .Where(x => x.Id == command.CommentId)
            .FirstOrDefaultAsync(ct);

        if (comment is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound, command.CommentId);

        bool authorized = await AuthorizePostAndComment(command, comment.PostId, ct);
        if (!authorized)
            return ("The author limits who can view their comments", HandlerResponseStatus.Unauthorized, comment.AuthorId);

        var result = comment.AddLike(new(command.UserId));
        if (!result.IsSuccess)
        {
            return result;
        }

        _context.Add(result.Payload);

        return HandlerResponseStatus.Created;
    }

    private async Task<bool> AuthorizePostAndComment(LikeCommentCommand command, PostId postId, CancellationToken ct)
    {
        return
             await _authorizationService
                .AuthorizeAsync<Post, PostId>(new(command.UserId), postId, new(command.IsAdmin), ct)
                &&
             await _authorizationService
                .AuthorizeAsync<Comment, CommentId>(new AuthorId(command.UserId), command.CommentId, new AuthOptions(command.IsAdmin), ct);
    }
}
