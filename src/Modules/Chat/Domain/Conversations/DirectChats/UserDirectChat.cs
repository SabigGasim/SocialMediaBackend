using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

public class UserDirectChat : AggregateRoot<UserDirectChatId>, IUserResource
{
    private readonly List<UserDirectMessage> _messages = new();

    private UserDirectChat() { }
    private UserDirectChat(ChatterId chatterId, DirectChatId directChatId)
    {
        Id = UserDirectChatId.New();
        ChatterId = chatterId;
        DirectChatId = directChatId;
    }

    public ChatterId ChatterId { get; private set; } = default!;
    public DirectChatId DirectChatId { get; private set; } = default!;

    public Chatter Chatter { get; private set; } = default!;
    public DirectChat DirectChat { get; private set; } = default!;
    public IReadOnlyCollection<UserDirectMessage> Messages => _messages.AsReadOnly();

    public static UserDirectChat Create(ChatterId chatterId, DirectChatId directChatId)
    {
        return new UserDirectChat(chatterId, directChatId);
    }

    public UserDirectMessage AddMessage(DirectMessageId messageId)
    {
        var message = UserDirectMessage.Create(messageId, this.Id);

        _messages.Add(message);

        return message;
    }

    public bool DeleteMessage(DirectMessageId messageId)
    {
        var message = _messages.Find(x => x.DirectMessageId == messageId);
        if (message is null)
        {
            return false;
        }

        _messages.Remove(message);

        return true;
    }
}
