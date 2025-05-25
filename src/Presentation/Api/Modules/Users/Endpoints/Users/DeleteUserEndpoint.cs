using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.DeleteUser;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints.Users;

[HttpDelete(ApiEndpoints.Users.Delete)]
public class DeleteUserEndpoint(IUsersModule module) : RequestEndpoint<DeleteUserRequest>(module)
{
    public override async Task HandleAsync(DeleteUserRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new DeleteUserCommand(req.UserId), ct);
    }
}
