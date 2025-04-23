using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Users.Follows.UnfollowUser;

namespace SocialMediaBackend.Api.Endpoints.Users.Follows;

[HttpDelete(ApiEndpoints.Users.Follow)]
public class UnfollowUserEndpoint : RequestEndpoint<UnfollowUserRequest>
{
    public override Task HandleAsync(UnfollowUserRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new UnfollowUserCommand(req.UserId), ct);
    }
}
