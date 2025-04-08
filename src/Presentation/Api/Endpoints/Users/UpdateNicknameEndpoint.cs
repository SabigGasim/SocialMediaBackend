using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Application.Users.UpdateNickname;

namespace SocialMediaBackend.Api.Endpoints.Users;

[HttpPatch(ApiEndpoints.Users.PatchNickname), AllowAnonymous]
internal class UpdateNicknameEndpoint : RequestEndpoint<UpdateNicknameRequest>
{
    public override async Task HandleAsync(UpdateNicknameRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new UpdateNicknameCommand(req.UserId, req.Nickname), ct);
    }
}
