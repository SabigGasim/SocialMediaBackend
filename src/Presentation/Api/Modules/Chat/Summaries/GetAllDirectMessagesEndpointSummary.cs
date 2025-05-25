using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.GetAllDirectMessages;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class GetAllDirectMessagesEndpointSummary : Summary<GetAllDirectMessagesEndpoint>
{
    public GetAllDirectMessagesEndpointSummary()
    {
        Description = "Retrieves all direct messages for the authenticated user.";
        Summary = "Get All Direct Messages";
        Response<GetAllDirectMessagesResponse>(200, "A list of direct messages for the user.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "User is not authorized to view this direct chat");
        Response<ErrorResponse>(404, "No direct chat with the specified Id was found or no direct messages found in this chat.");
    }
}
