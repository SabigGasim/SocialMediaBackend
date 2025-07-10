using SocialMediaBackend.Modules.Chat.Domain.Authorization;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Roles;

public class ChatterRole
{
    public Chat.Domain.Authorization.Roles RoleId { get; private set; } = default!;
    public ChatterId ChatterId { get; private set; } = default!;
    
    public Role Role { get; private set; } = default!;
    public Chatter Chatter { get; private set; } = default!;

    private ChatterRole() { }
    public ChatterRole(Chat.Domain.Authorization.Roles roleId, ChatterId chatterId)
    {
        RoleId = roleId;
        ChatterId = chatterId;
    }
}
