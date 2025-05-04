using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Posts.CreatePost;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Posts;

[HttpPost(ApiEndpoints.Posts.Create)]
public class CreatePostEndpoint : RequestEndpoint<CreatePostRequest, CreatePostResponse>
{
    public override Task HandleAsync(CreatePostRequest req, CancellationToken ct)
    {
        var command = new CreatePostCommand(req.Text, req.MediaItems);
        
        return HandleRequestAsync(command, ct);
    }
}
