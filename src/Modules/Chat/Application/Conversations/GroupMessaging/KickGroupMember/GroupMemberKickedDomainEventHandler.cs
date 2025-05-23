using Mediator;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Events;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.KickGroupMember;

public class GroupMemberKickedDomainEventHandler(ChatDbContext context) : INotificationHandler<GroupMemberKickedDomainEvent>
{
    private readonly ChatDbContext _context = context;

    public async ValueTask Handle(GroupMemberKickedDomainEvent notification, CancellationToken cancellationToken)
    {
        var userGroupChat = await _context.UserGroupChats
            .Where(x => x.ChatterId == notification.MemberId)
            .FirstOrDefaultAsync();

        userGroupChat?.Leave();
    }
}
