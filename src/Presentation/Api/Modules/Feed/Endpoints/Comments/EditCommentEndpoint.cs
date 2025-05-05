using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.EditComment;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

[HttpPatch(ApiEndpoints.Comments.Patch)]
public class EditCommentEndpoint : RequestEndpoint<EditCommentRequest>
{
    public override Task HandleAsync(EditCommentRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new EditCommentCommand(req.CommentId, req.Text), ct);
    }
}
