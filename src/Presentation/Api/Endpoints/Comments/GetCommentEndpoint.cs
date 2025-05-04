using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Comments.GetComment;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Comments;

[HttpGet(ApiEndpoints.Comments.Get)]
public class GetCommentEndpoint : RequestEndpoint<GetCommentRequest, GetCommentResponse>
{
    public override Task HandleAsync(GetCommentRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetCommentQuery(req.CommentId), ct);
    }
}
