using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.DeleteComment;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

[HttpDelete(ApiEndpoints.Posts.DeleteComment)]
public class DeleteCommentEndpoint : RequestEndpoint<DeleteCommentRequest>
{
    public override Task HandleAsync(DeleteCommentRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new DeleteCommentCommand(req.CommentId, req.PostId), ct);
    }
}
