using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.Users.Follows;

public class FollowUserEndpoint(IUsersModule module) : RequestEndpoint<FollowUserRequest, FollowUserResponse>(module)
{
    public override void Configure()
    {
        Post(ApiEndpoints.Users.Follow);
        Description(x => x.Accepts<FollowUserRequest>());
    }

    public override Task HandleAsync(FollowUserRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new FollowUserCommand(req.UserId), ct);
    }
}
