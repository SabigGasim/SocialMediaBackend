using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.LikeComment;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

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