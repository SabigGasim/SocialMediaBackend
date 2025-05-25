using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;
using SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class CreateGroupChatEndpointSummary : Summary<CreateGroupChatEndpoint>
{
    public CreateGroupChatEndpointSummary()
    {
        Summary = "Create a new group chat";
        Description = "This endpoint allows users to create a new group chat by providing the necessary details.";
        Response<CreateGroupChatResponse>(201, "Group chat created successfully.");
        Response<ErrorResponse>(400, "Bad request. Invalid input data.");
        Response<ErrorResponse>(401, "User is not authenticated.");
    }
}
