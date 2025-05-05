using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetComment;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

[HttpGet(ApiEndpoints.Comments.Get)]
public class GetCommentEndpoint : RequestEndpoint<GetCommentRequest, GetCommentResponse>
{
    public override Task HandleAsync(GetCommentRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetCommentQuery(req.CommentId), ct);
    }
}
