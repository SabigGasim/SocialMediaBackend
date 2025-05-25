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
