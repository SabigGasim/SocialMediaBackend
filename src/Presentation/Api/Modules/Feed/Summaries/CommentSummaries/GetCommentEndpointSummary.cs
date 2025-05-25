using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetComment;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.CommentSummaries;

public class GetCommentEndpointSummary : Summary<GetCommentEndpoint>
{
    public GetCommentEndpointSummary()
    {
        Summary = "Retrieves a specific comment by its ID.";
        Description = "Fetches the details of a single comment using its unique identifier.";
        Response<GetCommentResponse>(200, "Comment retrieved successfully.");
        Response<ErrorResponse>(403, "This user is not authorized to view this comment.");
        Response<ErrorResponse>(401, "This user is not authenticated.");
        Response<ErrorResponse>(404, "Comment not found.");
    }
}