﻿using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Users.DeleteUser;

namespace SocialMediaBackend.Api.Endpoints.Users;

[HttpDelete(ApiEndpoints.Users.Delete), AllowAnonymous]
public class DeleteUserEndpoint : RequestEndpoint<DeleteUserRequest>
{
    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new DeleteUserCommand(req.UserId), ct);
    }
}


public class DeleteUserEndpointSummary : Summary<DeleteUserEndpoint>
{
    public DeleteUserEndpointSummary()
    {
        Summary = "Deletes a user from the system";
        Description = "Deletes a user from the system";
        Response(204, "The user was deleted successfully");
        Response(404, "A user with this Id was not found");
    }
}
