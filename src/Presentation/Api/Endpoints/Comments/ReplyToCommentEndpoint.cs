using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Comments.ReplyToComment;

namespace SocialMediaBackend.Api.Endpoints.Comments;

[HttpPost(ApiEndpoints.Comments.Reply)]
public class ReplyToCommentEndpoint : RequestEndpoint<ReplyToCommentRequest, ReplyToCommentResponse>
{
    public override Task HandleAsync(ReplyToCommentRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new ReplyToCommentCommand(req.ParentId, req.UserId, req.Text), ct);
    }
}
