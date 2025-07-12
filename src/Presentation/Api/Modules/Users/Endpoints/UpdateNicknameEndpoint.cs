using FastEndpoints;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.Users.UpdateNickname;

namespace SocialMediaBackend.Api.Modules.Users.Endpoints;

[HttpPatch(ApiEndpoints.Users.PatchNickname)]
internal class UpdateNicknameEndpoint(IUsersModule module) : RequestEndpoint<UpdateNicknameRequest>(module)
{
    public override async Task HandleAsync(UpdateNicknameRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new UpdateNicknameCommand(req.Nickname), ct);
    }
}