using Mediator;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Events;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.LeaveGroupChat;

public class GroupChatLeftDomainEventHandler(ChatDbContext context)
    : INotificationHandler<GroupChatLeftDomainEvent>
{
    private readonly ChatDbContext _context = context;

    public async ValueTask Handle(GroupChatLeftDomainEvent notification, CancellationToken cancellationToken)
    {
        var userGroupChat = await _context.UserGroupChats
            .Where(x => x.GroupChatId == notification.GroupChatId && x.ChatterId == notification.MemberId)
            .FirstAsync(x => x.GroupChatId == notification.GroupChatId);

        userGroupChat?.Leave();
    }
}
