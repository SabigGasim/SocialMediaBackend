using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;
using SocialMediaBackend.Modules.Feed.Application.Posts.GetPost;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.PostSummaries;

public class GetPostEndpointSummary : Summary<GetPostEndpoints>
{
    public GetPostEndpointSummary()
    {
        Summary = "Get a post by ID";
        Description = "Retrieves a specific post from the feed using its unique identifier.";
        Response<GetPostResponse>(200, "The post was successfully retrieved.");
        Response<ErrorResponse>(404, "The post with the specified ID was not found.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "User is not authorized to view this post.");
    }
}
