using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Users.Application.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Mappings;
using SocialMediaBackend.Modules.Users.Domain.Feed.Posts;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Comments.CreateComment;

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
            .AuthorizeAsync(new(command.UserId), command.PostId, new(command.IsAdmin), ct);

        if (!authorized)
            return ("The author limits who can view their posts", HandlerResponseStatus.Unauthorized, post.AuthorId);

        var comment = post.AddComment(command.Text, new(command.UserId));

        _context.Add(comment);
        await _context.SaveChangesAsync(ct);

        return (comment.MapToCreateResponse(), HandlerResponseStatus.Created);
    }
}
