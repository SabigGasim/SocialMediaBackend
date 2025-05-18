using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats.Events;

public class DirectMessageAddedDomainEvent(
    DirectMessageId messageId,
    DirectChatId chatId,
    ChatterId senderId,
    ChatterId receiverId,
    string text,
    DateTimeOffset sentAt,
    MessageStatus status) : DomainEventBase
{
    public DirectMessageId MessageId { get; } = messageId;
    public DirectChatId ChatId { get; } = chatId;
    public ChatterId SenderId { get; } = senderId;
    public ChatterId ReceiverId { get; } = receiverId;
    public string Text { get; } = text;
    public DateTimeOffset SentAt { get; } = sentAt;
    public MessageStatus Status { get; } = status;
}
