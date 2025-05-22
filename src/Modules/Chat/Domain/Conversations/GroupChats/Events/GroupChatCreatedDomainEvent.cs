using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Events;

public class GroupChatCreatedDomainEvent(
    GroupChatId groupChatId,
    DateTimeOffset joinedAt,
    IEnumerable<ChatterId> membersIds) : DomainEventBase
{
    public GroupChatId GroupChatId { get; } = groupChatId;
    public DateTimeOffset JoinedAt { get; } = joinedAt;
    public IEnumerable<ChatterId> Members { get; } = membersIds;
}
