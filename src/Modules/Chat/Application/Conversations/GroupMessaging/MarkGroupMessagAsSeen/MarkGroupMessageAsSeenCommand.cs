using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessagAsSeen;

public class MarkGroupMessageAsSeenCommand(Guid groupId, Guid messageId) : CommandBase, IUserRequest
{
    public Guid UserId { get; private set; }

    public bool IsAdmin { get; private set; }
    public GroupMessageId GroupMessageId { get; } = new(messageId);
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
