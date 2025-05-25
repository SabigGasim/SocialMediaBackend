using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectChat;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class CreateDirectChatEndpointSummary : Summary<CreateDirectChatEndpoint>
{
    public CreateDirectChatEndpointSummary()
    {
        Summary = "Create a direct chat between two users";
        Description = "This endpoint allows the creation of a direct chat between two users. It requires the IDs of both users to be provided in the request body.";
        Response<CreateDirectChatResponse>(201, "Chat created successfully.");
        Response<ErrorResponse>(400, "Invalid request data.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(404, "User was not found.");
    }
}
