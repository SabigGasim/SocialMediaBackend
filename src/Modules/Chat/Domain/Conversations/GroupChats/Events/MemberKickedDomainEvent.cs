using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Events;

public class MemberKickedDomainEvent(ChatterId memberId) : DomainEventBase
{
    public ChatterId MemberId { get; } = memberId;
}
