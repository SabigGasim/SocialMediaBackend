using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Posts.CreatePost;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;

[HttpPost(ApiEndpoints.Posts.Create)]
public class CreatePostEndpoint : RequestEndpoint<CreatePostRequest, CreatePostResponse>
{
    public override Task HandleAsync(CreatePostRequest req, CancellationToken ct)
    {
        var command = new CreatePostCommand(req.Text, req.MediaItems);

        return HandleRequestAsync(command, ct);
    }
}
