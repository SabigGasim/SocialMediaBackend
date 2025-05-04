using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Comments.DeleteComment;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Comments;

[HttpDelete(ApiEndpoints.Posts.DeleteComment)]
public class DeleteCommentEndpoint : RequestEndpoint<DeleteCommentRequest>
{
    public override Task HandleAsync(DeleteCommentRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new DeleteCommentCommand(req.CommentId, req.PostId), ct);
    }
}
