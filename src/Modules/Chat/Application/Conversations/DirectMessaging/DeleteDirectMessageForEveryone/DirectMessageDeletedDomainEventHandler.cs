using Mediator;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats.Events;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.DirectMessaging.DeleteDirectMessageForEveryone;

public class DirectMessageDeletedDomainEventHandler(ChatDbContext context) : INotificationHandler<DirectMessageDeletedDomainEvent>
{
    private readonly ChatDbContext _context = context;

    public async ValueTask Handle(DirectMessageDeletedDomainEvent notification, CancellationToken cancellationToken)
    {
        var userChats = await _context.UserDirectChats
            .Where(x => x.Messages.Where(x => x.DirectMessageId == notification.MessageId).Any())
            .ToListAsync(cancellationToken);

        foreach (var chat in userChats)
        {
            chat.DeleteMessage(notification.MessageId);
        }
    }
}
