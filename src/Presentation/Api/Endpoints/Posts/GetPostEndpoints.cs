using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Posts.GetPost;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Posts;

[HttpGet(ApiEndpoints.Posts.Get)]
public class GetPostEndpoints : RequestEndpoint<GetPostRequest, GetPostResponse>
{
    public override Task HandleAsync(GetPostRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetPostQuery(req.PostId), ct);
    }
}
