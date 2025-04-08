using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Users.UpdateNickname;
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
