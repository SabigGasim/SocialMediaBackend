using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
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
