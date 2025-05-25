using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;
using SocialMediaBackend.Modules.Feed.Application.Posts.GetAllPosts;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.PostSummaries;

public class GetAllPostsEndpointSummary : Summary<GetAllPostsEndpoints>
{
    public GetAllPostsEndpointSummary()
    {
        Summary = "Get all posts";
        Description = "Retrieves a list of all posts from the feed.";
        Response<GetAllPostsResponse>(200, "A list of posts retrieved successfully.");
        Response<ErrorResponse>(400, "Bad request. The request was invalid or cannot be served.");
        Response<ErrorResponse>(403, "Forbidden occurs when the user provides a specific UserId to the endpoint where the user is not authorized to view.");
    }
}
