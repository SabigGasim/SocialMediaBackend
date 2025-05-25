using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class DeleteGroupMessageForEveryoneEndpointSummary : Summary<DeleteGroupMessageForEveryoneEndpoint>
{
    public DeleteGroupMessageForEveryoneEndpointSummary()
    {
        Summary = "Delete a group message for everyone";
        Description = "This endpoint allows a user to delete a group message for all members of the group. " +
                      "It requires the user to be the author of the message and the group ID to be provided in the request body.";
        Response(204, "The message was successfully deleted for all members of the group.");
        Response<ErrorResponse>(400, "Bad request. The request was invalid or missing required parameters.");
        Response<ErrorResponse>(404, "Not found. The specified group or message does not exist.");
        Response<ErrorResponse>(403, "Forbidden. The user is not authorized to delete this message.");
        Response<ErrorResponse>(401, "User is not authenticated.");
    }
}
