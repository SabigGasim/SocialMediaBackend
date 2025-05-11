using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Contracts;
using SocialMediaBackend.Modules.Feed.Application.Posts.GetPost;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;

[HttpGet(ApiEndpoints.Posts.Get)]
public class GetPostEndpoints(IFeedModule module) : RequestEndpoint<GetPostRequest, GetPostResponse>(module)
{
    public override Task HandleAsync(GetPostRequest req, CancellationToken ct)
    {
        return HandleQueryAsync(new GetPostQuery(req.PostId), ct);
    }
}
