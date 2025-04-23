using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Users.Privacy.ChangeProfileVisibility;

namespace SocialMediaBackend.Api.Endpoints.Users.Privacy;

[HttpPatch(ApiEndpoints.Users.Privacy.ChangeProfileVisibility)]
public class ChangeProfileVisibilityEndpoint : RequestEndpoint<ChangeProfileVisibilityRequest>
{
    public override Task HandleAsync(ChangeProfileVisibilityRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new ChangeProfileVisibilityCommand(req.IsPublic), ct);
    }
}
