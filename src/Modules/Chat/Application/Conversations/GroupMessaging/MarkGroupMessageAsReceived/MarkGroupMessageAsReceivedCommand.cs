using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.MarkGroupMessageAsReceived;

public class MarkGroupMessageAsReceivedCommand(Guid groupChatId, Guid messageId) : CommandBase, IUserRequest
{
    public GroupChatId GroupChatId { get; } = new(groupChatId);
    public GroupMessageId MessageId { get; } = new(messageId);

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
