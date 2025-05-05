using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Posts.DeletePost;
using SocialMediaBackend.Modules.Users.Api.Endpoints;

namespace SocialMediaBackend.Api.Endpoints.Posts;

[HttpDelete(ApiEndpoints.Posts.Delete)]
public class DeletePostEndpoint : RequestEndpoint<DeletePostRequest>
{
    public override Task HandleAsync(DeletePostRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new DeletePostCommand(req.PostId), ct);
    }
}
