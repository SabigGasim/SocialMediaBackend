using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Posts.CreatePost;

namespace SocialMediaBackend.Api.Endpoints.Posts;

[HttpPost(ApiEndpoints.Posts.Create), AllowAnonymous]
public class CreatePostEndpoint : RequestEndpoint<CreatePostRequest, CreatePostResponse>
{
    public override Task HandleAsync(CreatePostRequest req, CancellationToken ct)
    {
        var userId = Guid.Parse("d6a319b0-5c31-4929-84a0-3b918efc318e");
        var command = new CreatePostCommand(userId, req.Text, req.MediaItems);
        
        return HandleRequestAsync(command, ct);
    }
}
