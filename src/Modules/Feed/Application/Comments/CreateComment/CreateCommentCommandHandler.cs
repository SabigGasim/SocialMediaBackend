using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Feed.Application.Auth;
using SocialMediaBackend.Modules.Feed.Application.Mappings;
using SocialMediaBackend.Modules.Feed.Domain.Posts;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Application.Comments.CreateComment;

public class CreateCommentCommandHandler(
    FeedDbContext context,
    IAuthorizationHandler<Post, PostId> authorizationHandler)
    : ICommandHandler<CreateCommentCommand, CreateCommentResponse>
{
    private readonly FeedDbContext _context = context;
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

        return (comment.MapToCreateResponse(), HandlerResponseStatus.Created);
    }
}
