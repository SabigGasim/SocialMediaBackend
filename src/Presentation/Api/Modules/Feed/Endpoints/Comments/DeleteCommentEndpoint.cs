using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.DeleteComment;
using SocialMediaBackend.Modules.Feed.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

[HttpDelete(ApiEndpoints.Posts.DeleteComment)]
public class DeleteCommentEndpoint(IFeedModule module) : RequestEndpoint<DeleteCommentRequest>(module)
{
    public override Task HandleAsync(DeleteCommentRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new DeleteCommentCommand(req.CommentId, req.PostId), ct);
    }
}
