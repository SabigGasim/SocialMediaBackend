using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

namespace SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

public class GroupMessage : AuditableEntity<GroupMessageId>
{
    private readonly List<Chatter> _seenBy = new();

    private GroupMessage() { }

    private GroupMessage(ChatterId senderId, GroupChatId chatId, string text, DateTimeOffset sentAt)
        : base()
    {
        Id = GroupMessageId.New();
        SenderId = senderId;
        ChatId = chatId;
        Text = text;
        SentAt = sentAt;

        var now = DateTimeOffset.UtcNow;

        Created = now;
        CreatedBy = senderId.ToString();
        LastModified = now;
        LastModifiedBy = senderId.ToString();
    }

    public ChatterId SenderId { get; private set; } = default!;
    public GroupChatId ChatId { get; private set; } = default!;
    public string Text { get; private set; } = default!;
    public DateTimeOffset SentAt { get; private set; }

    public Chatter Sender {  get; private set; } = default!;
    public GroupChat Chat { get; private set; } = default!;
    public IReadOnlyCollection<Chatter> SeenBy => _seenBy.AsReadOnly();

    public static GroupMessage Create(
        ChatterId senderId,
        GroupChatId chatId,
        string text,
        DateTimeOffset sentAt)
    {
        return new GroupMessage(senderId, chatId, text, sentAt);
    }
}
