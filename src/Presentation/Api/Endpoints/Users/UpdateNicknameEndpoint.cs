using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using SocialMediaBackend.Api.Abstractions;
using SocialMediaBackend.Api.Contracts.Responses;
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