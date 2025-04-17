using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Posts.DeletePost;

namespace SocialMediaBackend.Api.Endpoints.Posts;

[HttpDelete(ApiEndpoints.Posts.Delete), AllowAnonymous]
public class DeletePostEndpoint : RequestEndpoint<DeletePostRequest>
{
    public override Task HandleAsync(DeletePostRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new DeletePostCommand(req.PostId), ct);
    }
}
