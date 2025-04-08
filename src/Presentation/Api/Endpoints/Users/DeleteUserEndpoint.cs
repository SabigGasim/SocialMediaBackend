using FastEndpoints;
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
