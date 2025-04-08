using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Users.GetAllUsers;

namespace SocialMediaBackend.Api.Endpoints.Users;

[HttpGet(ApiEndpoints.Users.GetAll), AllowAnonymous]
public class GetAllUsersEndpoint : RequestEndpoint<GetAllUsersRequest, GetAllUsersResponse>
{
    public override async Task HandleAsync(GetAllUsersRequest req, CancellationToken ct)
    {
        await HandleRequestAsync(new GetAllUsersQuery(req.Slug, req.PageNumber, req.PageSize), ct);
    }
}
