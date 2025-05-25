using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.PostSummaries;

public class UpdatePostEndpointSummary : Summary<UpdatePostEndpoint>
{
    public UpdatePostEndpointSummary()
    {
        Summary = "Update a post";
        Description = "Updates the content of an existing post.";
        Response(204, "Post updated successfully.");
        Response<ErrorResponse>(400, "Invalid request data.");
        Response<ErrorResponse>(404, "Post not found.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "User is not authorized to edit this post.");
    }
}
