using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupMessage;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class SendGroupMessageEndpointSummary : Summary<SendGroupMessageEndpoint>
{
    public SendGroupMessageEndpointSummary()
    {
        Summary = "Send a message to a group chat";
        Description = "This endpoint allows users to send messages to a specific group chat identified by its ID.";
        Response<SendGroupMessageResponse>(201, "Message sent successfully.");
        Response<ErrorResponse>(400, "Bad request. The request was invalid or cannot be served.");
        Response<ErrorResponse>(404, "Not found. The specified group chat does not exist.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "User is not authorized to view this group.");
    }
}
