using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;
using SocialMediaBackend.Modules.Feed.Application.Comments.ReplyToComment;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.CommentSummaries;

public class ReplyToCommentEndpointSummary : Summary<ReplyToCommentEndpoint>
{
    public ReplyToCommentEndpointSummary()
    {
        Summary = "Replies to an existing comment.";
        Description = "Creates a reply to a specified parent comment.";
        Response<ReplyToCommentResponse>(201, "Reply created successfully.");
        Response<ErrorResponse>(404, "Parent comment not found.");
        Response<ErrorResponse>(400, "Invalid request data.");
        Response<ErrorResponse>(403, "User is not authorized to interact with the parent comment.");
        Response<ErrorResponse>(401, "User is not authenticated.");
    }
}
