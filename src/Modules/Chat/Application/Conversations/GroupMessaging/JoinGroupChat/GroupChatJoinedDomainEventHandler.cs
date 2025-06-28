using Mediator;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Events;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.JoinGroupChat;

internal sealed class GroupChatJoinedDomainEventHandler(ChatDbContext context)
    : INotificationHandler<GroupChatJoinedDomainEvent>
{
    private readonly ChatDbContext _context = context;

    public async ValueTask Handle(GroupChatJoinedDomainEvent notification, CancellationToken cancellationToken)
    {
        var userGroupChat = await _context.UserGroupChats
            .Where(x => x.GroupChatId == notification.GroupChatId && x.ChatterId == notification.MemberId)
            .FirstOrDefaultAsync(x => x.GroupChatId == notification.GroupChatId);

        if (userGroupChat is null)
        {
            var group = UserGroupChat.CreateJoined(notification.MemberId, notification.GroupChatId);
            
            _context.UserGroupChats.Add(group);
            
            return;
        }

        userGroupChat.Join();
    }
}
