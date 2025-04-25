using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Application.Abstractions.Requests;
using SocialMediaBackend.Application.Abstractions.Requests.Commands;
using SocialMediaBackend.Application.Common;
using SocialMediaBackend.Application.Mappings;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Comments.CreateComment;

public class CreateCommentHandler(
    ApplicationDbContext context,
    IAuthorizationHandler<Post, PostId> authorizationHandler)
    : ICommandHandler<CreateCommentCommand, CreateCommentResponse>
{
    private readonly ApplicationDbContext _context = context;
    private readonly IAuthorizationHandler<Post, PostId> _authorizationHandler = authorizationHandler;

    public async Task<HandlerResponse<CreateCommentResponse>> ExecuteAsync(CreateCommentCommand command, CancellationToken ct)
    {
        var post = await _context.Posts.FindAsync([command.PostId], ct);
        if (post is null)
            return ("Post with the given Id was not found", HandlerResponseStatus.NotFound, command.PostId);

        var authorized = await _authorizationHandler
            .AuthorizeAsync(command.UserId, command.PostId, new(command.IsAdmin), ct);

        if (!authorized)
            return ("The author limits who can view their posts", HandlerResponseStatus.Unauthorized, post.UserId);

        var comment = post.AddComment(command.Text, command.UserId);

        _context.Add(comment);
        await _context.SaveChangesAsync(ct);

        return (comment.MapToCreateResponse(), HandlerResponseStatus.Created);
    }
}
