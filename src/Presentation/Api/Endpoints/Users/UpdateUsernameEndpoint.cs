﻿using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Contracts.Responses;
using SocialMediaBackend.Application.Users.UpdateUsername;

namespace SocialMediaBackend.Api.Endpoints.Users;

[HttpPatch(ApiEndpoints.Users.PatchUsername), AllowAnonymous]
internal class UpdateUsernameEndpoint : RequestEndpoint<UpdateUsernameRequest>
{
    public override async Task HandleAsync(UpdateUsernameRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new UpdateUsernameCommand(req.UserId, req.Username), ct);
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