using Mediator;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats.Events;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.CreateDirectChat;

public class DirectChatCreatedDomainEventHandler : INotificationHandler<DirectChatCreatedDomainEvent>
{
    private readonly ChatDbContext _context;

    public DirectChatCreatedDomainEventHandler(ChatDbContext context) => _context = context;

    public ValueTask Handle(DirectChatCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var firstUserDirectChat = UserDirectChat.Create(notification.FirstChatterId, notification.ChatId);
        var secondUserDirectChat = UserDirectChat.Create(notification.SecondChatterId, notification.ChatId);

        _context.AddRange(firstUserDirectChat, secondUserDirectChat);

        return ValueTask.CompletedTask;
    }
}
