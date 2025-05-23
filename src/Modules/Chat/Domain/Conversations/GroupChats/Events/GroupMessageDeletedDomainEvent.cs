using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Events;

public class GroupMessageDeletedDomainEvent(GroupMessageId messageId) : DomainEventBase
{
    public GroupMessageId MessageId { get; } = messageId;
}
