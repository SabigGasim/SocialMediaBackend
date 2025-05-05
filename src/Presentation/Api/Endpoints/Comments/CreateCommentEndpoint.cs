using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.CreateComment;
using SocialMediaBackend.Modules.Users.Api.Endpoints;

namespace SocialMediaBackend.Api.Endpoints.Comments;

[HttpPost(ApiEndpoints.Posts.Comment)]
public class CreateCommentEndpoint : RequestEndpoint<CreateCommentRequest, CreateCommentResponse>
{
    public override Task HandleAsync(CreateCommentRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new CreateCommentCommand(req.PostId, req.Text), ct);
    }
}
