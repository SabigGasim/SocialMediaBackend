using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Comments.GetComment;

namespace SocialMediaBackend.Api.Endpoints.Comments;

[HttpGet(ApiEndpoints.Comments.Get), AllowAnonymous]
public class GetCommentEndpoint : RequestEndpoint<GetCommentRequest, GetCommentResponse>
{
    public override Task HandleAsync(GetCommentRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetCommentQuery(req.CommentId), ct);
    }
}
