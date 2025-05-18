using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.DirectChats;

namespace SocialMediaBackend.Modules.Chat.Domain.Messages.DirectMessages;

public class UserDirectMessage : Entity<UserDirectMessageId>
{
    private UserDirectMessage() { }

    private UserDirectMessage(DirectMessageId directMessageId, UserDirectChatId chatId)
    {
        Id = UserDirectMessageId.New();
        DirectMessageId = directMessageId;
        UserDirectChatId = chatId;
    }

    public DirectMessageId DirectMessageId { get; private set; } = default!;
    public UserDirectChatId UserDirectChatId { get; private set; } = default!;

    public DirectMessage DirectMessage { get; private set; } = default!;
    public UserDirectChat UserDirectChat { get; private set; } = default!;

    internal static UserDirectMessage Create(DirectMessageId directMessageId, UserDirectChatId chatId)
    {
        return new UserDirectMessage(directMessageId, chatId);
    }
}
