using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Feed.Application.Comments.UnlikeComment;
using SocialMediaBackend.Modules.Feed.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

public class UnlikeCommentEndpoint(IFeedModule module) : RequestEndpoint<UnlikeCommentRequest>(module)
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