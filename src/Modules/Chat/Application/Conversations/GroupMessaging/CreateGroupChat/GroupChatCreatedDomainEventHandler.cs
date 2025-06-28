using Mediator;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Events;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Application.Conversations.GroupMessaging.CreateGroupChat;

internal sealed class GroupChatCreatedDomainEventHandler(ChatDbContext context) : INotificationHandler<GroupChatCreatedDomainEvent>
{
    private readonly ChatDbContext _context = context;

    public ValueTask Handle(GroupChatCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        foreach (var memberId in notification.Members)
        {
            var userGroupChat = UserGroupChat.CreateJoined(memberId, notification.GroupChatId);

            _context.Add(userGroupChat);
        }

        return ValueTask.CompletedTask;
    }
}
