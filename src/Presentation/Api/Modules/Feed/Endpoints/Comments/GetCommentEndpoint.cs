using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetComment;
using SocialMediaBackend.Modules.Feed.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

[HttpGet(ApiEndpoints.Comments.Get)]
public class GetCommentEndpoint(IFeedModule module) : RequestEndpoint<GetCommentRequest, GetCommentResponse>(module)
{
    public override Task HandleAsync(GetCommentRequest req, CancellationToken ct)
    {
        return HandleQueryAsync(new GetCommentQuery(req.CommentId), ct);
    }
}
