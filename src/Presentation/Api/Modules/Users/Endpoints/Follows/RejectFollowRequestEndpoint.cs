﻿using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.Follows.RejectFollowRequest;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.Follows;

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
