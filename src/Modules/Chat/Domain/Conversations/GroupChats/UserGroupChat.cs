using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

public class UserGroupChat : AggregateRoot<UserGroupChatId>
{
    private readonly List<UserGroupMessage> _messages = new();

    private UserGroupChat() { }

    private UserGroupChat(ChatterId chatterId, GroupChatId groupChatId)
    {
        Id = UserGroupChatId.New();
        ChatterId = chatterId;
        GroupChatId = groupChatId;

        Join();
    }

    public ChatterId ChatterId { get; private set; } = default!;
    public GroupChatId GroupChatId { get; private set; } = default!;
    public bool IsJoined { get; private set; }

    public Chatter Chatter { get; private set; } = default!;
    public GroupChat GroupChat { get; private set; } = default!;

    public IReadOnlyCollection<UserGroupMessage>? Messages => _messages?.AsReadOnly();

    public static UserGroupChat CreateJoined(ChatterId chatterId, GroupChatId groupChatId)
    {
        return new UserGroupChat(chatterId, groupChatId);
    }

    public UserGroupMessage AddMessage(GroupMessageId messageId)
    {
        var message = UserGroupMessage.Create(messageId, this.Id);

        _messages.Add(message);

        return message;
    }

    public void Join()
    {
        CheckRule(new GroupChatMustNotBeAlreadyJoinedRule(IsJoined));

        IsJoined = true;
    }

    public void Leave()
    {
        CheckRule(new GroupChatMustNotBeAlreadyLeftRule(IsJoined));

        IsJoined = false;
    }
}
