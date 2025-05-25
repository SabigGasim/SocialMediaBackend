using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class LeaveGroupChatEndpointSummary : Summary<LeaveGroupChatEndpoint>
{
    public LeaveGroupChatEndpointSummary()
    {
        Summary = "Leave a group chat";
        Description = "Allows a user to leave an existing group chat by providing the chat ID.";
        Response(204, "Successfully left the group chat.");
        Response<ErrorResponse>(400, "Bad request. The provided chat ID is invalid.");
        Response<ErrorResponse>(404, "Not found. The specified group chat does not exist.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(409, "User isn't a member of this group");
    }
}
