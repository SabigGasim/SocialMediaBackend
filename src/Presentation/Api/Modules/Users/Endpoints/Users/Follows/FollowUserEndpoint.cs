using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.Users.Follows;

public class FollowUserEndpoint : RequestEndpoint<FollowUserRequest, FollowUserResponse>
{
    public override void Configure()
    {
        Post(ApiEndpoints.Users.Follow);
        Description(x => x.Accepts<FollowUserRequest>());
    }

    public override Task HandleAsync(FollowUserRequest req, CancellationToken ct)
    {
        return HandleRequestAsync(new FollowUserCommand(req.UserId), ct);
    }
}
