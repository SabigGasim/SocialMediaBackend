using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.RejectFollowRequet;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.Users.Follows;

public class RejectFollowRequestEndpoint(IUsersModule module) : RequestEndpoint<RejectFollowRequestRequest>(module)
{
    public override void Configure()
    {
        Post(ApiEndpoints.Users.RejectFollow);
        Description(x => x.Accepts<RejectFollowRequestRequest>());
    }

    public override Task HandleAsync(RejectFollowRequestRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new RejectFollowRequestCommand(req.UserId), ct);
    }
}
