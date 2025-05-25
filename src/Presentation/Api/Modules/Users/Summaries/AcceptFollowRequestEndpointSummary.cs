using FastEndpoints;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Users.Follows;

namespace SocialMediaBackend.Api.Modules.Users.Summaries;

public class AcceptFollowRequestEndpointSummary : Summary<AcceptFollowRequestEndpoint>
{
    public AcceptFollowRequestEndpointSummary()
    {
        Summary = "Accepts a follow request from another user.";
        Description = "This endpoint allows a user to accept a follow request from another user.";
        Response(204, "The follow request was successfully accepted.");
        Response<ErrorResponse>(400, "The request was invalid.");
        Response<ErrorResponse>(404, "The follow request was not found or the user does not exist.");
    }
}
