using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class MarkGroupMessageAsReceivedEndpointSummary : Summary<MarkGroupMessageAsReceivedEndpoint>
{
    public MarkGroupMessageAsReceivedEndpointSummary()
    {
        Summary = "Marks a group message as received by the current user.";
        Description = "This endpoint allows a user to mark a specific group message as received.  It updates the message status to indicate that the user has read the message.";
        Response(200, "Message marked as received successfully.");
        Response(400, "Invalid request data.");
        Response(404, "Group not found.");
        Response(401, "User is not authenticated.");
        Response(403, "User is not authorized to view this group.");
    }
}
