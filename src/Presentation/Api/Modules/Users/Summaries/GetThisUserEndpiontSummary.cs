using FastEndpoints;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Users;
using SocialMediaBackend.Modules.Users.Application.Users.GetFullUserDetails;

namespace SocialMediaBackend.Api.Modules.Users.Summaries;

public class GetThisUserEndpiontSummary : Summary<GetThisUserEndpiont>
{
    public GetThisUserEndpiontSummary()
    {
        Summary = "Get the details of the currently authenticated user";
        Description = "Retrieves the full details of the user who is currently authenticated, including their profile information and any other relevant data.";
        Response<GetFullUserDetailsResponse>(200, "The details of the authenticated user.");
        Response(401, "Unauthorized - The request is not authenticated.");
    }
}
