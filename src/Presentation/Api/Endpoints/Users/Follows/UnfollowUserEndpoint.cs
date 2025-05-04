using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.UnfollowUser;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Users.Follows;

[HttpDelete(ApiEndpoints.Users.Follow)]
public class UnfollowUserEndpoint : RequestEndpoint<UnfollowUserRequest>
{
    public override Task HandleAsync(UnfollowUserRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new UnfollowUserCommand(req.UserId), ct);
    }
}
