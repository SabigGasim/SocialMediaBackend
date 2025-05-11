using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Contracts;
using SocialMediaBackend.Modules.Feed.Application.Posts.DeletePost;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;

[HttpDelete(ApiEndpoints.Posts.Delete)]
public class DeletePostEndpoint(IFeedModule module) : RequestEndpoint<DeletePostRequest>(module)
{
    public override Task HandleAsync(DeletePostRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new DeletePostCommand(req.PostId), ct);
    }
}
