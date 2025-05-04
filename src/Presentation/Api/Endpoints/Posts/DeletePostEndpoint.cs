using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Posts.DeletePost;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Posts;

[HttpDelete(ApiEndpoints.Posts.Delete)]
public class DeletePostEndpoint : RequestEndpoint<DeletePostRequest>
{
    public override Task HandleAsync(DeletePostRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new DeletePostCommand(req.PostId), ct);
    }
}
