using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.KickGroupMember;

[HasPermission(Permissions.KickGroupMember)]
public sealed class KickGroupMemberCommand(Guid chatterId, Guid groupChatId)
    : GroupCommandBase<KickGroupMemberMessage>, IUserRequest

{
    public ChatterId ChatterId { get; } = new(chatterId);
    public GroupChatId GroupChatId { get; } = new(groupChatId);

    public Guid UserId { get; private set; }

    public bool IsAdmin { get; private set; }

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
