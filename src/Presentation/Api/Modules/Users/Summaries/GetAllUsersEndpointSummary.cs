using FastEndpoints;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Users;
using SocialMediaBackend.Modules.Users.Application.Users.GetAllUsers;

namespace SocialMediaBackend.Api.Modules.Users.Summaries;

public class GetAllUsersEndpointSummary : Summary<GetAllUsersEndpoint>
{
    public GetAllUsersEndpointSummary()
    {
        Summary = "Gets multiple users from the system";
        Description = "Gets a paginated response of users in the system that match the given criteria";
        Response<GetAllUsersResponse>(200, "A paginated response of users with the given criteria were returned");
    }
}
