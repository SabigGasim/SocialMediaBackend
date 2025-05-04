using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Comments.EditComment;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Comments;

[HttpPatch(ApiEndpoints.Comments.Patch)]
public class EditCommentEndpoint : RequestEndpoint<EditCommentRequest>
{
    public override Task HandleAsync(EditCommentRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new EditCommentCommand(req.CommentId, req.Text), ct);
    }
}
