using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;
using SocialMediaBackend.Modules.Feed.Application.Comments.GetAllPostComments;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.CommentSummaries;

public class GetAllPostCommentsEndpointSummary : Summary<GetAllPostCommentsEndpoint>
{
    public GetAllPostCommentsEndpointSummary()
    {
        Summary = "Retrieves all comments for a specific post.";
        Description = "Fetches a paginated list of comments associated with the specified post.";
        Response<GetAllPostCommentsResponse>(200, "Comments retrieved successfully.");
        Response<ErrorResponse>(404, "Post or comment was not found.");
        Response<ErrorResponse>(401, "Unauthorized access. User must be logged in to view comments.");
        Response<ErrorResponse>(403, "Forbidden access. User does not have permission to view comments on this post.");
    }
}
