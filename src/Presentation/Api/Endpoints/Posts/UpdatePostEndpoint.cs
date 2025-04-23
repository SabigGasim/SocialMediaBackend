using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Posts.UpdatePost;

namespace SocialMediaBackend.Api.Endpoints.Posts;

[HttpPatch(ApiEndpoints.Posts.Patch)]
public class UpdatePostEndpoint : RequestEndpoint<UpdatePostRequest>
{
    public override Task HandleAsync(UpdatePostRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new UpdatePostCommand(req.PostId, req.Text), ct);
    }
}
