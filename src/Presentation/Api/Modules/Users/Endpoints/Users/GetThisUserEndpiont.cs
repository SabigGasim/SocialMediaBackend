using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.GetFullUserDetails;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.Users;

[HttpGet(ApiEndpoints.Users.Me)]
public class GetThisUserEndpiont(IUsersModule module) : RequestEndpointWithoutRequest<GetFullUserDetailsResponse>(module)
{
    public override Task HandleAsync(CancellationToken ct)
    {
        return HandleQueryAsync(new GetFullUserDetailsQuery(), ct);
    }
}
