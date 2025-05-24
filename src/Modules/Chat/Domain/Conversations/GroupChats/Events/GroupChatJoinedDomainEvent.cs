using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Events;

public class GroupChatJoinedDomainEvent(
    GroupChatId groupChatId,
    ChatterId memberId) : DomainEventBase
{
    public GroupChatId GroupChatId { get; } = groupChatId;
    public ChatterId MemberId { get; } = memberId;
}
