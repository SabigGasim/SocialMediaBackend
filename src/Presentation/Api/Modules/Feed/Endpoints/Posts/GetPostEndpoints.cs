using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Posts.GetPost;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;

[HttpGet(ApiEndpoints.Posts.Get)]
public class GetPostEndpoints : RequestEndpoint<GetPostRequest, GetPostResponse>
{
    public override Task HandleAsync(GetPostRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new GetPostQuery(req.PostId), ct);
    }
}
