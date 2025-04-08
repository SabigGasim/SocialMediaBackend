using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Users.GetUser;

namespace SocialMediaBackend.Api.Endpoints.Users;

[HttpGet(ApiEndpoints.Users.Get), AllowAnonymous]
internal class GetUserEndpoint : RequestEndpoint<GetUserRequest, GetUserResponse>
{
    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        await HandleRequestAsync(new GetUserQuery(req.IdOrUsername), ct);
    }
}
