using FastEndpoints;
using SocialMediaBackend.Modules.Users.Api.Abstractions;
using SocialMediaBackend.Modules.Users.Api.Contracts.Responses;
using SocialMediaBackend.Modules.Users.Application.Users.UpdateNickname;

namespace SocialMediaBackend.Modules.Users.Api.Endpoints.Users;

[HttpPatch(ApiEndpoints.Users.PatchNickname)]
internal class UpdateNicknameEndpoint : RequestEndpoint<UpdateNicknameRequest>
{
    public override async Task HandleAsync(UpdateNicknameRequest req, CancellationToken ct)
    {
        await HandleCommandAsync(new UpdateNicknameCommand(req.Nickname), ct);
    }
}


internal class UpdateNicknameEndpointSummary : Summary<UpdateNicknameEndpoint>
{
    public UpdateNicknameEndpointSummary()
    {
        Summary = "Updates the nickname of a given user";
        Description = "Updates the nickname of a given user";
        Response<ValidationFailureResponse>(400, "The given nickname format isn't valid");
        Response(404, "No user with the given id was found");
        Response(204, "Nickname updated successfully");
    }
}