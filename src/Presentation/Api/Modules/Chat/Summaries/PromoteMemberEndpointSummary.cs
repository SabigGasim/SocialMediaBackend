using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class PromoteMemberEndpointSummary : Summary<PromoteMemberEndpoint>
{
    public PromoteMemberEndpointSummary()
    {
        Summary = "Promote a member to admin in a chat";
        Description = "This endpoint allows an admin to promote a member to admin status in a chat.";
        Response(204, "Member promoted successfully");
        Response<ErrorResponse>(400, "Bad request - invalid input or member not found");
        Response<ErrorResponse>(401, "User is not authenticated");
        Response<ErrorResponse>(403, "Forbidden - user isn't authorized to access this group or isn't authorized to promote members");
        Response<ErrorResponse>(404, "Not found - chat does not exist or specified user isn't a member of this chat");
    }
}
