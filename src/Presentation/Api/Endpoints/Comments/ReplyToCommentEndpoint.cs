using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Comments.ReplyToComment;

namespace SocialMediaBackend.Api.Endpoints.Comments;

[HttpPost(ApiEndpoints.Comments.Reply), AllowAnonymous]
public class ReplyToCommentEndpoint : RequestEndpoint<ReplyToCommentRequest, ReplyToCommentResponse>
{
    public override Task HandleAsync(ReplyToCommentRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new ReplyToCommentCommand(req.ParentId, req.UserId, req.Text), ct);
    }
}
