using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Comments.CreateComment;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Comments;

[HttpPost(ApiEndpoints.Posts.Comment)]
public class CreateCommentEndpoint : RequestEndpoint<CreateCommentRequest, CreateCommentResponse>
{
    public override Task HandleAsync(CreateCommentRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new CreateCommentCommand(req.PostId, req.Text), ct);
    }
}
