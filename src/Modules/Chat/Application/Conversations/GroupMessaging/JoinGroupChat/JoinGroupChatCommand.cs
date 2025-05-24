using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.JoinGroupChat;

public class JoinGroupChatCommand(Guid groupChatId)
    : GroupCommandBase<GroupChatJoinedMessage>, IUserRequest
{
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
