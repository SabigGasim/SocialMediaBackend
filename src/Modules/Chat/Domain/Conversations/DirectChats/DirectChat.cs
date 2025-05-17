using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

public class DirectChat : AggregateRoot<DirectChatId>
{
    private readonly List<DirectMessage>? _messages;

    private DirectChat() { }

    private DirectChat(ChatterId firstChatterId, ChatterId secondChatterId, DateTimeOffset createdAt) : base()
    {
        Id = DirectChatId.New();
        FirstChatterId = firstChatterId;
        SecondChatterId = secondChatterId;
        CreatedAt = createdAt;
    }

    public ChatterId FirstChatterId { get; private set; } = default!;
    public ChatterId SecondChatterId { get; private set; } = default!;
    public DateTimeOffset CreatedAt { get; private set; }

    public Chatter FirstChatter { get; private set; } = default!;
    public Chatter SecondChatter { get; private set; } = default!;
    public IReadOnlyCollection<DirectMessage>? Messages => _messages?.AsReadOnly();
    
    public static DirectChat Create(
        ChatterId firstChatterId, 
        ChatterId secondChatterId, 
        DateTimeOffset createdAt)
    {
        return new DirectChat(firstChatterId, secondChatterId, createdAt);
    }
}
