using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Posts.UnlikePost;
using SocialMediaBackend.Modules.Users.Api.Endpoints;

namespace SocialMediaBackend.Api.Endpoints.Posts;

public class UnlikePostEndpoint : RequestEndpoint<UnlikePostRequest>
{
    public override void Configure()
    {
        Delete(ApiEndpoints.Posts.Unlike);
        Description(x => x.Accepts<UnlikePostRequest>());
    }

    public override Task HandleAsync(UnlikePostRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new UnlikePostCommand(req.PostId), ct);
    }
}
