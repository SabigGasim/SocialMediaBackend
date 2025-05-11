using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Contracts;
using SocialMediaBackend.Modules.Feed.Application.Posts.UnlikePost;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;

public class UnlikePostEndpoint(IFeedModule module) : RequestEndpoint<UnlikePostRequest>(module)
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
