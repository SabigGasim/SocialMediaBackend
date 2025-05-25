using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.CommentSummaries;

public class LikeCommentEndpointSummary : Summary<LikeCommentEndpoint>
{
    public LikeCommentEndpointSummary()
    {
        Summary = "Like a comment";
        Description = "Allows a user to like a specific comment on a post.";
        Response(201, "Comment liked successfully.");
        Response<ErrorResponse>(404, "Comment not found.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "User is not authorized to view this comment.");
        Response<ErrorResponse>(400, "Invalid request parameters.");
        Response<ErrorResponse>(409, "Comment is already liked.");
    }
}
