using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.CommentSummaries;

public class UnlikeCommentEndpointSummary : Summary<UnlikeCommentEndpoint>
{
    public UnlikeCommentEndpointSummary()
    {
        Summary = "Unlike a comment on a post";
        Description = "Allows users to remove their like from a comment they previously liked.";
        Response(204, "Successfully unliked the comment.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "User is not authorized to view this comment.");
        Response<ErrorResponse>(400, "Invalid request parameters.");
        Response<ErrorResponse>(409, "Comment is not liked.");
    }
}
