using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

namespace SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

public class UserDirectMessage : Entity<UserDirectMessageId>
{
    private UserDirectMessage() { }

    private UserDirectMessage(ChatterId senderId, DirectMessageId directMessageId, UserDirectChatId chatId)
    {
        Id = UserDirectMessageId.New();
        SenderId = senderId;
        DirectMessageId = directMessageId;
        UserDirectChatId = chatId;
    }

    public ChatterId SenderId { get; private set; } = default!;
    public DirectMessageId DirectMessageId { get; private set; } = default!;
    public UserDirectChatId UserDirectChatId { get; private set; } = default!;

    public Chatter Sender { get; private set; } = default!;
    public DirectMessage DirectMessage { get; private set; } = default!;
    public UserDirectChat UserDirectChat { get; private set; } = default!;

    internal static UserDirectMessage Create(
        ChatterId senderId, 
        DirectMessageId directMessageId,
        UserDirectChatId chatId)
    {
        return new UserDirectMessage(senderId, directMessageId, chatId);
    }
}
