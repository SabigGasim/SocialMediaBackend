using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Comments.ReplyToComment;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Comments;

[HttpPost(ApiEndpoints.Comments.Reply)]
public class ReplyToCommentEndpoint : RequestEndpoint<ReplyToCommentRequest, ReplyToCommentResponse>
{
    public override Task HandleAsync(ReplyToCommentRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new ReplyToCommentCommand(req.ParentId, req.Text), ct);
    }
}
