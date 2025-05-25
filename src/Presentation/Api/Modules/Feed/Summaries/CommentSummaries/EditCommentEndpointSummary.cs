using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.CommentSummaries;

public class EditCommentEndpointSummary : Summary<EditCommentEndpoint>
{
    public EditCommentEndpointSummary()
    {
        Summary = "Edit a comment";
        Description = "Allows users to edit their existing comments.";
        Response(204, "Comment edited successfully.");
        Response<ErrorResponse>(404, "Comment not found.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "User is not authorized to view this comment.");
        Response<ErrorResponse>(400, "Invalid request parameters.");
    }
}
