using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Users.Follows.RejectFollowRequet;

namespace SocialMediaBackend.Api.Endpoints.Users.Follows;

public class RejectFollowRequestEndpoint : RequestEndpoint<RejectFollowRequestRequest>
{
    public override void Configure()
    {
        Post(ApiEndpoints.Users.RejectFollow);
        Description(x => x.Accepts<RejectFollowRequestRequest>());
        AllowAnonymous();
    }

    public override Task HandleAsync(RejectFollowRequestRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new RejectFollowRequestCommand(req.UserId), ct);
    }
}
