using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Infrastructure.Data;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Auth;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Domain.Feed.Posts;
using SocialMediaBackend.Domain.Feed.Comments;

namespace SocialMediaBackend.Application.Comments.LikeComment;
internal class LikeCommentCommandHandler(
    ApplicationDbContext context,
    IAuthorizationService authorizationService) : ICommandHandler<LikeCommentCommand>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    public async Task<HandlerResponse> ExecuteAsync(LikeCommentCommand command, CancellationToken ct)
    {
        var comment = await _context.Comments
            .Include(x => x.User)
            .Include(x => x.Likes.Where(p => p.UserId == command.UserId))
            .Where(x => x.Id == command.CommentId)
            .FirstOrDefaultAsync(ct);

        if (comment is null)
            return ("Comment with the given Id was not found", HandlerResponseStatus.NotFound, command.CommentId);
        
        bool authorized = await AuthorizePostAndComment(command, comment.PostId, ct);
        if (!authorized)
            return ("The author limits who can view their comments", HandlerResponseStatus.Unauthorized, comment.UserId);

        var like = comment.AddLike(command.UserId);
        if (like is null)
            return ("User already liked this comment", HandlerResponseStatus.Conflict, comment.UserId);

        _context.Add(like);
        await _context.SaveChangesAsync(ct);

        return HandlerResponseStatus.Created;
    }

    private async Task<bool> AuthorizePostAndComment(LikeCommentCommand command, PostId postId, CancellationToken ct)
    {
        return 
             await _authorizationService
                .AuthorizeAsync<Post, PostId>(command.UserId, postId, new(command.IsAdmin), ct)
                &&
             await _authorizationService
                .AuthorizeAsync<Comment, CommentId>(command.UserId, command.CommentId, new(command.IsAdmin), ct);
    }
}
