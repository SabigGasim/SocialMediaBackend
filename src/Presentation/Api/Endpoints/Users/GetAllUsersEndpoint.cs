using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Users.GetAllUsers;

namespace SocialMediaBackend.Api.Endpoints.Users;

[HttpGet(ApiEndpoints.Users.GetAll)]
public class GetAllUsersEndpoint : RequestEndpoint<GetAllUsersRequest, GetAllUsersResponse>
{
    public override async Task HandleAsync(GetAllUsersRequest req, CancellationToken ct)
    {
        await HandleRequestAsync(new GetAllUsersQuery(req.Slug, req.Page, req.PageSize), ct);
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
