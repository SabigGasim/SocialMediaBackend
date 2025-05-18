using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats.Events;
public class DirectChatCreatedDomainEvent(
    DirectChatId chatId,
    ChatterId firstChatterId,
    ChatterId secondChatterId,
    DateTimeOffset createdAt) : DomainEventBase
{
    public DirectChatId ChatId { get; } = chatId;
    public ChatterId FirstChatterId { get; } = firstChatterId;
    public ChatterId SecondChatterId { get; } = secondChatterId;
    public DateTimeOffset CreatedAt { get; } = createdAt;
}