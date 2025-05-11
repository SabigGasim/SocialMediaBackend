using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.Privacy.ChangeProfileVisibility;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.Users.Privacy;

[HttpPatch(ApiEndpoints.Users.Privacy.ChangeProfileVisibility)]
public class ChangeProfileVisibilityEndpoint(IUsersModule module) : RequestEndpoint<ChangeProfileVisibilityRequest>(module)
{
    public override Task HandleAsync(ChangeProfileVisibilityRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new ChangeProfileVisibilityCommand(req.IsPublic), ct);
    }
}
