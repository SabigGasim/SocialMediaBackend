using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Posts.LikePost;
using SocialMediaBackend.Modules.Users.Api.Endpoints;

namespace SocialMediaBackend.Api.Endpoints.Posts;

public class LikePostEndpoint : RequestEndpoint<LikePostRequest>
{
    public override void Configure()
    {
        Post(ApiEndpoints.Posts.Like);
        Description(x => x.Accepts<LikePostRequest>());
    }

    public override Task HandleAsync(LikePostRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new LikePostCommand(req.PostId), ct);
    }
}
