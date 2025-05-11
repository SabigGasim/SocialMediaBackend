using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.GetAllUsers;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.Users;

[HttpGet(ApiEndpoints.Users.GetAll)]
public class GetAllUsersEndpoint(IUsersModule module) : RequestEndpoint<GetAllUsersRequest, GetAllUsersResponse>(module)
{
    public override async Task HandleAsync(GetAllUsersRequest req, CancellationToken ct)
    {
        await HandleQueryAsync(new GetAllUsersQuery(req.Slug, req.Page, req.PageSize), ct);
    }
}


public class GetAllUsersEndpointSummary : Summary<GetAllUsersEndpoint>
{
    public GetAllUsersEndpointSummary()
    {
        Summary = "Gets multiple users from the system";
        Description = "Gets a paginated response of users in the system that match the given criteria";
        Response<GetAllUsersResponse>(200, "A paginated response of users with the given criteria were returned");
    }
}
