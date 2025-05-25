using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class DeleteDirectMessageForEveyroneEndpointSummary : Summary<DeleteDirectMessageForEveyroneEndpoint>
{
    public DeleteDirectMessageForEveyroneEndpointSummary()
    {
        Description = "Deletes a direct message for everyone in the chat.";
        Summary = "Delete Direct Message for Everyone";
        Response(204, "The direct message was successfully deleted for everyone.");
        Response<ErrorResponse>(404, "The direct message was not found.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "User is not authorized to view this direct chat or direct message or isn't authorized to delete the message.");
    }
}   
