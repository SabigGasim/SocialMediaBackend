using FastEndpoints;
using SocialMediaBackend.Api.Modules.Users.Endpoints;

namespace SocialMediaBackend.Api.Modules.Users.Summaries;

public class DeleteUserEndpointSummary : Summary<DeleteUserEndpoint>
{
    public DeleteUserEndpointSummary()
    {
        Summary = "Deletes a user from the system";
        Description = "Deletes a user from the system";
        Response(204, "The user was deleted successfully");
        Response(404, "A user with this Id was not found");
    }
}
