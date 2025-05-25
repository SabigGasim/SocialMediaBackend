using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetAllReplies;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.CommentSummaries;

public class GetAllRepliesEndpointSummary : Summary<GetAllRepliesEndpoint>
{
    public GetAllRepliesEndpointSummary()
    {
        Summary = "Get all replies to a comment";
        Description = "Retrieves all replies to a specific comment, including pagination support.";
        Response<GetAllRepliesResponse>(200, "Replies retrieved successfully");
        Response<ErrorResponse>(404, "Comment not found");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "User is not authorized to view this comment.");
    }
}
