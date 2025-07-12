using FastEndpoints;
using SocialMediaBackend.Api.Contracts;
using SocialMediaBackend.Api.Modules.Users.Endpoints;

namespace SocialMediaBackend.Api.Modules.Users.Summaries;

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