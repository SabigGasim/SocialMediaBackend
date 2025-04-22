using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Users.GetFullUserDetails;

namespace SocialMediaBackend.Api.Endpoints.Users;

[HttpGet(ApiEndpoints.Users.Me), AllowAnonymous]
public class GetThisUserEndpiont : RequestEndpointWithoutRequest<GetFullUserDetailsResponse>
{
    public override Task HandleAsync(CancellationToken ct)
    {
        return HandleRequestAsync(new GetFullUserDetailsQuery(), ct);
    }
}
