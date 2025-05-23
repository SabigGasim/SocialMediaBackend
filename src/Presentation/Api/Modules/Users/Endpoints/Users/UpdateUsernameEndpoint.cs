﻿using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Contracts;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.UpdateUsername;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.Users;

[HttpPatch(ApiEndpoints.Users.PatchUsername)]
internal class UpdateUsernameEndpoint(IUsersModule module) : RequestEndpoint<UpdateUsernameRequest>(module)
{
    public override async Task HandleAsync(UpdateUsernameRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new UpdateUsernameCommand(req.Username), ct);
    }
}



internal class UpdateUsernameEndpointSummary : Summary<UpdateUsernameEndpoint>
{
    public UpdateUsernameEndpointSummary()
    {
        Summary = "Updates the username of a given user";
        Description = "Updates the username of a given user";
        Response<ValidationFailureResponse>(400, "The given username format isn't valid");
        Response(409, "Username is already taken");
        Response(204, "Username was updated successfully");
    }
}