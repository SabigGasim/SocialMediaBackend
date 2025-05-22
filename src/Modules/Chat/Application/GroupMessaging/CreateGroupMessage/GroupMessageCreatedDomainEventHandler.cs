using Mediator;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Events;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.GroupMessaging.CreateGroupMessage;

public class GroupMessageCreatedDomainEventHandler(ChatDbContext context)
    : INotificationHandler<GroupMessageCreatedDomainEvent>
{
    private readonly ChatDbContext _context = context;

    public async ValueTask Handle(GroupMessageCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var userGroupChats = await _context.UserGroupChats
            .Where(x => x.GroupChatId == notification.GroupChatId && x.IsJoined)
            .ToListAsync(cancellationToken);

        foreach (var group in  userGroupChats)
        {
            var message = group.AddMessage(notification.MessageId);
            
            _context.UserGroupMessages.Add(message);
        }
    }
}
