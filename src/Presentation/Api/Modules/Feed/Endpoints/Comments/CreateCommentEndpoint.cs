using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.CreateComment;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

[HttpPost(ApiEndpoints.Posts.Comment)]
public class CreateCommentEndpoint : RequestEndpoint<CreateCommentRequest, CreateCommentResponse>
{
    public override Task HandleAsync(CreateCommentRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new CreateCommentCommand(req.PostId, req.Text), ct);
    }
}
