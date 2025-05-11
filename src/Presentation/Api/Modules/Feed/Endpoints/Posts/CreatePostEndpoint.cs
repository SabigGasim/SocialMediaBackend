using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Contracts;
using SocialMediaBackend.Modules.Feed.Application.Posts.CreatePost;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;

[HttpPost(ApiEndpoints.Posts.Create)]
public class CreatePostEndpoint(IFeedModule module) : RequestEndpoint<CreatePostRequest, CreatePostResponse>(module)
{
    public override Task HandleAsync(CreatePostRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new CreatePostCommand(req.Text, req.MediaItems), ct);
    }
}
