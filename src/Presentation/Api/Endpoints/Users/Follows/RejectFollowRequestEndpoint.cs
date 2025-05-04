using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.RejectFollowRequet;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Users.Follows;

public class RejectFollowRequestEndpoint : RequestEndpoint<RejectFollowRequestRequest>
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
