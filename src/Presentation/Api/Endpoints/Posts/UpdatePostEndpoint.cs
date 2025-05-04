using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Posts.UpdatePost;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Posts;

[HttpPatch(ApiEndpoints.Posts.Patch)]
public class UpdatePostEndpoint : RequestEndpoint<UpdatePostRequest>
{
    public override Task HandleAsync(UpdatePostRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new UpdatePostCommand(req.PostId, req.Text), ct);
    }
}
