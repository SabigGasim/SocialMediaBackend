using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Comments.CreateComment;

namespace SocialMediaBackend.Api.Endpoints.Comments;

[HttpPost(ApiEndpoints.Posts.Comment), AllowAnonymous]
public class CreateCommentEndpoint : RequestEndpoint<CreateCommentRequest, CreateCommentResponse>
{
    public override Task HandleAsync(CreateCommentRequest req, CancellationToken ct)
    {
        var userId = Guid.Parse("de18fec2-aabe-403e-9a5c-ee219203cf5e");
        return HandleRequestAsync(new CreateCommentCommand(userId, req.PostId, req.Text), ct);
    }
}
