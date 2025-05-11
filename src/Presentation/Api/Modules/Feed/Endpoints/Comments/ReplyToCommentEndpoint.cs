using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.ReplyToComment;
using SocialMediaBackend.Modules.Feed.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

[HttpPost(ApiEndpoints.Comments.Reply)]
public class ReplyToCommentEndpoint(IFeedModule module) : RequestEndpoint<ReplyToCommentRequest, ReplyToCommentResponse>(module)
{
    public override Task HandleAsync(ReplyToCommentRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new ReplyToCommentCommand(req.ParentId, req.Text), ct);
    }
}
