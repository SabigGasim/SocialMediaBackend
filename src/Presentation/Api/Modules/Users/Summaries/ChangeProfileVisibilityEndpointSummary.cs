using FastEndpoints;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Privacy;

namespace SocialMediaBackend.Api.Modules.Users.Summaries;

public class ChangeProfileVisibilityEndpointSummary : Summary<ChangeProfileVisibilityEndpoint>  
{
    public ChangeProfileVisibilityEndpointSummary()
    {
        Summary = "Change the visibility of the user's profile";
        Description = "Allows the user to change their profile visibility settings, making their profile either public or private.";
        Response(204, "Profile visibility changed successfully.");
        Response(400, "Bad Request - The request was invalid or could not be processed.");
        Response(401, "Unauthorized - The request is not authenticated.");
    }
}
