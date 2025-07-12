using FastEndpoints;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Follows;

namespace SocialMediaBackend.Api.Modules.Users.Summaries;

public class RejectFollowRequestEndpointSummary : Summary<RejectFollowRequestEndpoint>
{
    public RejectFollowRequestEndpointSummary()
    {
        Summary = "Rejects a follow request from another user";
        Description = "This endpoint allows a user to reject a follow request they have received from another user.";
        Response(204, "The follow request was successfully rejected.");
        Response<ErrorResponse>(400, "Bad Request: The request was invalid or could not be processed.");
        Response<ErrorResponse>(404, "Not Found: The specified follow request does not exist.");
    }
}
