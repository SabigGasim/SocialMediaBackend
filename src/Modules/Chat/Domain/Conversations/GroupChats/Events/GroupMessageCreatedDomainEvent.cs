using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Events;

public class GroupMessageCreatedDomainEvent(
    GroupChatId groupChatId,
    GroupMessageId messageId) : DomainEventBase()
{
    public GroupChatId GroupChatId { get; } = groupChatId;
    public GroupMessageId MessageId { get; } = messageId;
}
