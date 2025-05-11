using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.CreateComment;
using SocialMediaBackend.Modules.Feed.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

[HttpPost(ApiEndpoints.Posts.Comment)]
public class CreateCommentEndpoint(IFeedModule module) : RequestEndpoint<CreateCommentRequest, CreateCommentResponse>(module)
{
    public override Task HandleAsync(CreateCommentRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new CreateCommentCommand(req.PostId, req.Text), ct);
    }
}
