using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Comments.EditComment;

namespace SocialMediaBackend.Api.Endpoints.Comments;

[HttpPatch(ApiEndpoints.Comments.Patch), AllowAnonymous]
public class EditCommentEndpoint : RequestEndpoint<EditCommentRequest>
{
    public override Task HandleAsync(EditCommentRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new EditCommentCommand(req.CommentId, req.Text), ct);
    }
}
