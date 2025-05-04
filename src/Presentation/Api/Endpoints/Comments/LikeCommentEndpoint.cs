using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Comments.LikeComment;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Comments;

public class LikeCommentEndpoint : RequestEndpoint<LikeCommentRequest>
{
    public override void Configure()
    {
        Post(ApiEndpoints.Comments.Like);
        Description(x => x.Accepts<LikeCommentRequest>());
    }

    public override Task HandleAsync(LikeCommentRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new LikeCommentCommand(req.CommentId), ct);
    }
}