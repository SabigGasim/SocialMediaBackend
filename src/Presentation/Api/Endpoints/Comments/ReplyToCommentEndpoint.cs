using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.ReplyToComment;
using SocialMediaBackend.Modules.Users.Api.Endpoints;

namespace SocialMediaBackend.Api.Endpoints.Comments;

[HttpPost(ApiEndpoints.Comments.Reply)]
public class ReplyToCommentEndpoint : RequestEndpoint<ReplyToCommentRequest, ReplyToCommentResponse>
{
    public override Task HandleAsync(ReplyToCommentRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new ReplyToCommentCommand(req.ParentId, req.Text), ct);
    }
}
