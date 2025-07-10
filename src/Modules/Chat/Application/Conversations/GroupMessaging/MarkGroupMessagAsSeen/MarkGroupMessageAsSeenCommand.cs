using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessagAsSeen;

[HasPermission(Permissions.MarkDirectMessageAsSeen)]
public class MarkGroupMessageAsSeenCommand(Guid groupId) : CommandBase<GroupMessageId?>, IUserRequest
{
    public Guid UserId { get; private set; }

    public bool IsAdmin { get; private set; }
    public GroupChatId GroupChatId { get; } = new(groupId);

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
