using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Posts;
using SocialMediaBackend.Modules.Feed.Application.Posts.CreatePost;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.PostSummaries;

public class CreatePostEndpointSummary : Summary<CreatePostEndpoint>
{
    public CreatePostEndpointSummary()
    {
        Summary = "Create a new post";
        Description = "This endpoint allows users to create a new post in the feed.";
        Response<CreatePostResponse>(201, "Post created successfully.");
        Response<ErrorResponse>(400, "Bad request. The input data is invalid.");
        Response<ErrorResponse>(401, "Unauthorized. User must be authenticated to create a post.");
    }
}
