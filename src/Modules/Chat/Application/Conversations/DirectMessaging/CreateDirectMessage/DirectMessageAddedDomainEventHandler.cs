using Mediator;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats.Events;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectMessage;

public class DirectMessageAddedDomainEventHandler(ChatDbContext context) : INotificationHandler<DirectMessageAddedDomainEvent>
{
    private readonly ChatDbContext _context = context;

    public async ValueTask Handle(DirectMessageAddedDomainEvent notification, CancellationToken cancellationToken)
    {
        var userDirectChats = await _context.UserDirectChats
            .Where(x => x.DirectChatId == notification.ChatId)
            .ToListAsync();

        var senderChat = userDirectChats.FirstOrDefault(x => x.ChatterId == notification.SenderId);
        var receiverChat = userDirectChats.FirstOrDefault(x => x.ChatterId == notification.ReceiverId);

        AddMessageToUserDirectChat(
            notification.MessageId,
            notification.ChatId,
            senderChat,
            notification.SenderId);

        AddMessageToUserDirectChat(
            notification.MessageId,
            notification.ChatId,
            receiverChat,
            notification.ReceiverId);
    }

    private void AddMessageToUserDirectChat(
        DirectMessageId messageId,
        DirectChatId directChatId,
        UserDirectChat? userDirectChat,
        ChatterId chatterId)
    {
        if (userDirectChat is null)
        {
            var chat = UserDirectChat.Create(chatterId, directChatId);
            userDirectChat = chat;

            _context.UserDirectChats.Add(chat);
        }

        var message = userDirectChat.AddMessage(messageId);

        _context.UserDirectMessages.Add(message);
    }
}
