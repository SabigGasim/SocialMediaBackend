using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.GetAllGroupMessages;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class GetAllGroupMessagesEndpointSummary : Summary<GetAllGroupMessagesEndpoint>
{
    public GetAllGroupMessagesEndpointSummary()
    {
        Summary = "Get all messages in a group chat";
        Description = "Retrieves all messages from a specific group chat, identified by the group ID.";
        Response<GetAllGroupMessagesResponse>(200, "Successfully retrieved messages");
        Response<ErrorResponse>(404, "Group not found");
        Response<ErrorResponse>(401, "User is not authenticated");
        Response<ErrorResponse>(404, "User is not authorized to view this group");
    }
}
