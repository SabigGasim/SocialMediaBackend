using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.CommentSummaries;

public class DeleteCommentEndpointSummary : Summary<DeleteCommentEndpoint>
{
    public DeleteCommentEndpointSummary()
    {
        Summary = "Delete a comment";
        Description = "Deletes a comment by its ID. The user must be the author of the comment or an admin to perform this action.";
        Response(204, "Comment deleted successfully.");
        Response<ErrorResponse>(401, "This user is not authenticated.");
        Response<ErrorResponse>(403, "Forbidden: You do not have permission to delete this comment.");
        Response<ErrorResponse>(404, "Not Found: The specified comment does not exist.");
        Response<ErrorResponse>(400, "Invalid request data.");
    }
}
