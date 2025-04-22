using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Users.Follows.AcceptFollowRequest;

namespace SocialMediaBackend.Api.Endpoints.Users.Follows;


public class AcceptFollowRequestEndpoint : RequestEndpoint<AcceptFollowRequestRequest>
{
    public override void Configure()
    {
        Post(ApiEndpoints.Users.AcceptFollow);
        Description(x => x.Accepts<AcceptFollowRequestRequest>());
        AllowAnonymous();
    }

    public override Task HandleAsync(AcceptFollowRequestRequest req, CancellationToken ct)
    {
        return HandleCommandAsync(new AcceptFollowRequetCommand(req.UserId), ct);
    }
}
