using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

namespace SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

public class DirectMessage : AuditableEntity<DirectMessageId>
{
    private DirectMessage() {}

    private DirectMessage(
        ChatterId senderId, 
        DirectChatId chatId,
        string text,
        DateTimeOffset sentAt, 
        MessageStatus status)
    {
        Id = DirectMessageId.New();
        SenderId = senderId;
        ChatId = chatId;
        Text = text;
        SentAt = sentAt;
        Status = status;

        var now = DateTimeOffset.UtcNow;

        Created = now;
        CreatedBy = senderId.ToString();
        LastModified = now;
        LastModifiedBy = senderId.ToString();
    }

    public ChatterId SenderId { get; private set; } = default!;
    public DirectChatId ChatId { get; private set; } = default!;
    public string Text { get; private set; } = default!;
    public DateTimeOffset SentAt { get; private set; }
    public MessageStatus Status { get; private set; }

    public Chatter Sender { get; private set; } = default!;
    public DirectChat Chat { get; private set; } = default!;

    public static DirectMessage Create(
        ChatterId senderId, 
        DirectChatId chatId, 
        string text, 
        DateTimeOffset sentAt,
        MessageStatus status)
    {
        return new DirectMessage(senderId, chatId, text, sentAt, status);
    }
}
