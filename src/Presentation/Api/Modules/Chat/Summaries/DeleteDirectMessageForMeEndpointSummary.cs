using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class DeleteDirectMessageForMeEndpointSummary : Summary<DeleteDirectMessageForMeEndpoint>
{
    public DeleteDirectMessageForMeEndpointSummary()
    {
        Description = "Deletes a direct message for the authenticated user.";
        Summary = "Delete Direct Message for Me";
        Response(204, "The direct message was successfully deleted for the user.");
        Response<ErrorResponse>(404, "The direct message was not found.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "User is not authorized to view this direct chat or direct message or isn't authorized to delete the message.");
    }
}
