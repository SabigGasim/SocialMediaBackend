using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.AcceptFollowRequest;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.Users.Follows;


public class AcceptFollowRequestEndpoint(IUsersModule module) : RequestEndpoint<AcceptFollowRequestRequest>(module)
{
    public override void Configure()
    {
        Post(ApiEndpoints.Users.AcceptFollow);
        Description(x => x.Accepts<AcceptFollowRequestRequest>());
    }

    public override Task HandleAsync(AcceptFollowRequestRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new AcceptFollowRequestCommand(req.UserId), ct);
    }
}
