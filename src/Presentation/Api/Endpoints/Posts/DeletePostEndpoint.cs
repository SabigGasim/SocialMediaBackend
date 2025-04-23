using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Posts.DeletePost;

namespace SocialMediaBackend.Api.Endpoints.Posts;

[HttpDelete(ApiEndpoints.Posts.Delete)]
public class DeletePostEndpoint : RequestEndpoint<DeletePostRequest>
{
    public override Task HandleAsync(DeletePostRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new DeletePostCommand(req.PostId), ct);
    }
}
