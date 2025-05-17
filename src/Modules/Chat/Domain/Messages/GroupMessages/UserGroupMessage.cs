using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

namespace SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

public class UserGroupMessage : Entity<UserGroupMessageId>
{
    private UserGroupMessage() { }

    private UserGroupMessage(ChatterId senderId, GroupMessageId groupMessageId, UserGroupChatId chatId)
    {
        Id = UserGroupMessageId.New();
        SenderId = senderId;
        GroupMessageId = groupMessageId;
        UserGroupChatId = chatId;
    }

    public ChatterId SenderId { get; private set; } = default!;
    public GroupMessageId GroupMessageId { get; private set; } = default!;
    public UserGroupChatId UserGroupChatId { get; private set; } = default!;

    public Chatter Sender { get; private set; } = default!;
    public GroupMessage GroupMessage { get; private set; } = default!;
    public UserGroupChat UserGroupChat { get; private set; } = default!;

    internal static UserGroupMessage Create(
        ChatterId senderId, 
        GroupMessageId groupMessageId,
        UserGroupChatId chatId)
    {
        return new UserGroupMessage(senderId, groupMessageId, chatId);
    }
}
