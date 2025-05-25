using FastEndpoints;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Users.Follows;

namespace SocialMediaBackend.Api.Modules.Users.Summaries;

public class UnfollowUserEndpointSummary : Summary<UnfollowUserEndpoint>
{
    public UnfollowUserEndpointSummary()
    {
        Summary = "Unfollow a user";
        Description = "Allows a user to unfollow another user, removing the follow relationship.";
        Response(204, "Successfully unfollowed the user.");
        Response<ErrorResponse>(404, "Not Found: User not found or not followed.");
        Response<ErrorResponse>(400, "Bad request: due to invalid input.");
    }
}
