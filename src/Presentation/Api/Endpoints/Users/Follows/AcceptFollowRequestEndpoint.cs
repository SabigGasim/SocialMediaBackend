using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.AcceptFollowRequest;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Users.Follows;


public class AcceptFollowRequestEndpoint : RequestEndpoint<AcceptFollowRequestRequest>
{
    public override void Configure()
    {
        Post(ApiEndpoints.Users.AcceptFollow);
        Description(x => x.Accepts<AcceptFollowRequestRequest>());
    }

    public override Task HandleAsync(AcceptFollowRequestRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new AcceptFollowRequetCommand(req.UserId), ct);
    }
}
