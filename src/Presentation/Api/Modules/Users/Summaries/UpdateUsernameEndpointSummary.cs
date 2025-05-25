using FastEndpoints;
using SocialMediaBackend.Api.Contracts;
using SocialMediaBackend.Api.Modules.Users.Endpoints.Users;

namespace SocialMediaBackend.Api.Modules.Users.Summaries;

internal class UpdateUsernameEndpointSummary : Summary<UpdateUsernameEndpoint>
{
    public UpdateUsernameEndpointSummary()
    {
        Summary = "Updates the username of a given user";
        Description = "Updates the username of a given user";
        Response<ValidationFailureResponse>(400, "The given username format isn't valid");
        Response(409, "Username is already taken");
        Response(204, "Username was updated successfully");
    }
}