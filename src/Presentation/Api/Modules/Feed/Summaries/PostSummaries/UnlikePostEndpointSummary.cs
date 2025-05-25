using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.PostSummaries;

public class UnlikePostEndpointSummary : Summary<UnlikePostEndpoint>
{
    public UnlikePostEndpointSummary()
    {
        Summary = "Unlike a post";
        Description = "Allows a user to remove their like from a post they previously liked.";
        Response(204, "Successfully unliked the post.");
        Response<ErrorResponse>(404, "Post not found or user has not liked the post.");
        Response<ErrorResponse>(400, "Bad request due to invalid input or state.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "User is not authorized to view this post.");
    }
}
