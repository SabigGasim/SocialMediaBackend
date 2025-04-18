using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Comments.GetAllPostComments;

namespace SocialMediaBackend.Api.Endpoints.Comments;

[HttpGet(ApiEndpoints.Posts.GetAllPostComments), AllowAnonymous]
public class GetAllPostCommentsEndpoint : RequestEndpoint<GetAllPostCommentsRequest, GetAllPostCommentsResponse>
{
    public override Task HandleAsync(GetAllPostCommentsRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetAllPostCommentsQuery(req.PostId, req.Page, req.PageSize), ct);
    }
}
