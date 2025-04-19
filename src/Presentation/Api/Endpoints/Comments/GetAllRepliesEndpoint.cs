using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Comments.GetAllReplies;

namespace SocialMediaBackend.Api.Endpoints.Comments;

[HttpGet(ApiEndpoints.Comments.GetReplies), AllowAnonymous]
public class GetAllRepliesEndpoint : RequestEndpoint<GetAllRepliesRequest, GetAllRepliesResponse>
{
    public override Task HandleAsync(GetAllRepliesRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetAllRepliesQuery(req.ParentId, req.Page, req.PageSize), ct);
    }
}
