using FastEndpoints;
using SocialMediaBackend.Api.Modules.Chat.Endpoints;

namespace SocialMediaBackend.Api.Modules.Chat.Summaries;

public class KickGroupMemberEndpointSummary : Summary<KickGroupMemberEndpoint>
{
    public KickGroupMemberEndpointSummary()
    {
        Summary = "Kick a member from a group";
        Description = "This endpoint allows an admin to remove a member from a group chat.";
        Response(204, "Member successfully kicked from the group.");
        Response<ErrorResponse>(400, "Bad request. The request was invalid or could not be processed.");
        Response<ErrorResponse>(401, "User is not authenticated.");
        Response<ErrorResponse>(403, "Forbidden. The user does not have permission to kick members from this group.");
        Response<ErrorResponse>(404, "Not found. The specified group or member does not exist.");
    }
}