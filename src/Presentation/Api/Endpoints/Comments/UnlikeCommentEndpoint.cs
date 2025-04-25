using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Comments.UnlikeComment;

namespace SocialMediaBackend.Api.Endpoints.Comments;

public class UnlikeCommentEndpoint : RequestEndpoint<UnlikeCommentRequest>
{
    public override void Configure()
    {
        Delete(ApiEndpoints.Comments.Unlike);
        Description(x => x.Accepts<UnlikeCommentRequest>());
    }

    public override Task HandleAsync(UnlikeCommentRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new UnlikeCommentCommand(req.CommentId), ct);
    }
}