using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class DeleteGroupMessageForMeEndpointSummary : Summary<DeleteGroupMessageForMeEndpoint>
{
    public DeleteGroupMessageForMeEndpointSummary()
    {
        Summary = "Delete a group message for the current user";
        Description = "This endpoint allows the current user to delete a group message from their view. The message will not be deleted for other users in the group.";
        Response(204, "The message was successfully deleted for the current user.");
        Response<ErrorResponse>(404, "The specified group or message was not found.");
        Response<ErrorResponse>(403, "You do not have permission to delete this message.");
        Response<ErrorResponse>(401, "User is not authenticated.");
    }
}
