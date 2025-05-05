using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetComment;
using SocialMediaBackend.Modules.Users.Api.Endpoints;

namespace SocialMediaBackend.Api.Endpoints.Comments;

[HttpGet(ApiEndpoints.Comments.Get)]
public class GetCommentEndpoint : RequestEndpoint<GetCommentRequest, GetCommentResponse>
{
    public override Task HandleAsync(GetCommentRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetCommentQuery(req.CommentId), ct);
    }
}
