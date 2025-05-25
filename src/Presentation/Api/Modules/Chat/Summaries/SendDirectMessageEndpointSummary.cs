using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;
using SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectMessage;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class SendDirectMessageEndpointSummary : Summary<SendDirectMessageEndpoint>
{
    public SendDirectMessageEndpointSummary()
    {
        Summary = "Send a direct message to a user";
        Description = "This endpoint allows you to send a direct message to another user in the social media application.";
        Response<SendDirectMessageResponse>(201, "Message sent successfully.");
        Response<ErrorResponse>(400, "Bad request. The request was invalid or cannot be served.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "User is not authorized to view this direct chat.");
        Response<ErrorResponse>(404, "Not found. The specified direct chat doesn't exist.");
    }
}
