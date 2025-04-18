using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Comments.DeleteComment;

namespace SocialMediaBackend.Api.Endpoints.Comments;

[HttpDelete(ApiEndpoints.Posts.DeleteComment), AllowAnonymous]
public class DeleteCommentEndpoint : RequestEndpoint<DeleteCommentRequest>
{
    public override Task HandleAsync(DeleteCommentRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new DeleteCommentCommand(req.CommentId, req.PostId), ct);
    }
}
