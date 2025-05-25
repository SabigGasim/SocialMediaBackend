using FastEndpoints;
using SocialMediaBackend.Api.Modules.Feed.Endpoints.Comments;
using SocialMediaBackend.Modules.Feed.Application.Comments.CreateComment;

namespace SocialMediaBackend.Api.Modules.Feed.Summaries.CommentSummaries;

public class CreateCommentEndpointSummary : Summary<CreateCommentEndpoint>
{
    public CreateCommentEndpointSummary()
    {
        Summary = "Creates a new comment on a post.";
        Description = "Adds a new comment to the specified post.";
        Response<CreateCommentResponse>(201, "Comment created successfully.");
        Response<ErrorResponse>(401, "This user is not authenticated.");
        Response<ErrorResponse>(400, "Invalid request data.");
    }
}