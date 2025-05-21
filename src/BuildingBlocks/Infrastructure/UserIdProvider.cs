using Microsoft.AspNetCore.SignalR;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public class UserIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
    {
        return connection.User.Claims.FirstOrDefault(x => x.Type == "userid")?.Value;
    }
}
