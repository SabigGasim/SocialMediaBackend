using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats.Events;

public class DirectMessageDeletedDomainEvent(DirectMessageId messageId) : DomainEventBase()
{
    public DirectMessageId MessageId { get; } = messageId;
}