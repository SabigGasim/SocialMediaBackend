using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.UnfollowUser;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.Follows;

[HttpDelete(ApiEndpoints.Users.Follow)]
public class UnfollowUserEndpoint(IUsersModule module) : RequestEndpoint<UnfollowUserRequest>(module)
{
    public override Task HandleAsync(UnfollowUserRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new UnfollowUserCommand(req.UserId), ct);
    }
}
