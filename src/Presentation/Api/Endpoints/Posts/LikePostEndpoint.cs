using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Posts.LikePost;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Posts;

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
