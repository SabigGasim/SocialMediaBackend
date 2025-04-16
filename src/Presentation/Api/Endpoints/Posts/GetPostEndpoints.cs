using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Posts.GetPost;

namespace SocialMediaBackend.Api.Endpoints.Posts;

[HttpGet(ApiEndpoints.Posts.Get), AllowAnonymous]
public class GetPostEndpoints : RequestEndpoint<GetPostRequest, GetPostResponse>
{
    public override Task HandleAsync(GetPostRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetPostQuery(req.PostId), ct);
    }
}
