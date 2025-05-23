using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.DeleteDirectMessageForEveryone;

public class DeleteDirectMessageForEveryoneCommand(Guid chatId, Guid messageId)
    : SingleUserCommandBase<DeleteDirectMessageMessage>, IUserRequest
{
    public Guid UserId { get; private set; }

    public bool IsAdmin { get; private set; }

    public DirectChatId ChatId { get; } = new(chatId);
    public DirectMessageId MessageId { get; } = new(messageId);

    public void WithAdminRole(bool isAdmin)
    {
        IsAdmin = isAdmin;
    }

    public void WithUserId(Guid userId)
    {
        UserId = userId;
    }
}
