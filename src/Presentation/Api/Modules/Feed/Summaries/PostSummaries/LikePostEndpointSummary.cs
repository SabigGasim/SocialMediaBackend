using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.PostSummaries;

public class LikePostEndpointSummary : Summary<LikePostEndpoint>
{
    public LikePostEndpointSummary()
    {
        Summary = "Like a post";
        Description = "Allows a user to like a specific post by its ID.";
        Response(201, "Post liked successfully.");
        Response<ErrorResponse>(404, "Post not found.");
        Response<ErrorResponse>(400, "Invalid request data.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "User is not authorized to view this post.");
    }
}
