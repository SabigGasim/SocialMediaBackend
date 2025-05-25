using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.PostSummaries;

public class DeletePostEndpointSummary : Summary<DeletePostEndpoint>
{
    public DeletePostEndpointSummary()
    {
        Summary = "Delete a post";
        Description = "Deletes a post by its ID. This endpoint requires authentication and the user must be the owner of the post.";
        Response(204, "Post deleted successfully.");
        Response<ErrorResponse>(404, "Post not found.");
        Response<ErrorResponse>(403, "Forbidden: You do not have permission to delete this post.");
        Response<ErrorResponse>(401, "User is not authenticated.");
    }
}
