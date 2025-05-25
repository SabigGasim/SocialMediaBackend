using FastEndpoints;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Users.Follows;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;

namespace SocialMediaBackend.Api.Modules.Users.Summaries;

public class FollowUserEndpointSummary : Summary<FollowUserEndpoint>
{
    public FollowUserEndpointSummary()
    {
        Summary = "Follow a user";
        Description = "Allows the authenticated user to follow another user by their ID.";
        Response<FollowUserResponse>(201, "Successfully followed the user.");
        Response<ErrorResponse>(400, "Bad request. The request was invalid or could not be processed.");
        Response<ErrorResponse>(401, "Unauthorized. The user is not authenticated.");
        Response<ErrorResponse>(404, "Not found. The specified user does not exist.");
        Response<ErrorResponse>(409, "The specified user is already followed");
    }
}
