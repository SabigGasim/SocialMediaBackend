using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.GetUser;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.Users;

[HttpGet(ApiEndpoints.Users.Get)]
internal class GetUserEndpoint(IUsersModule module) : RequestEndpoint<GetUserRequest, GetUserResponse>(module)
{
    public override async Task HandleAsync(GetUserRequest req, CancellationToken ct)
    {
        await HandleQueryAsync(new GetUserQuery(req.IdOrUsername), ct);
    }
}
