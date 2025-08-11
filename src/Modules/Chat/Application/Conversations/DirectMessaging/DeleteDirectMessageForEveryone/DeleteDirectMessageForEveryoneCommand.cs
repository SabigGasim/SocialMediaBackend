using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands.Realtime;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.DeleteDirectMessageForEveryone;

[HasPermission(Permissions.DeleteDirectMessageForEveryone)]
public class DeleteDirectMessageForEveryoneCommand(Guid chatId, Guid messageId)
    : SingleUserCommandBase<DeleteDirectMessageMessage>, IRequireAuthorization
{
    public DirectChatId ChatId { get; } = new(chatId);
    public DirectMessageId MessageId { get; } = new(messageId);
}
