using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

public class UserGroupChat : AggregateRoot<UserGroupChatId>
{
    private readonly List<DateTimeOffset> _joinLeaveTimeline = new();
    private List<UserGroupMessage>? _messages = default!;

    private UserGroupChat() { }

    private UserGroupChat(ChatterId chatterId, GroupChatId groupChatId, DateTimeOffset joinedAt)
    {
        Id = UserGroupChatId.New();
        ChatterId = chatterId;
        GroupChatId = groupChatId;

        Join(joinedAt);
    }

    public ChatterId ChatterId { get; private set; } = default!;
    public GroupChatId GroupChatId { get; private set; } = default!;
    public bool IsJoined { get; private set; }

    public Chatter Chatter { get; private set; } = default!;
    public GroupChat GroupChat { get; private set; } = default!;

    public IReadOnlyCollection<UserGroupMessage>? Messages => _messages?.AsReadOnly();
    public IReadOnlyCollection<DateTimeOffset> JoinLeaveTimeline => _joinLeaveTimeline.AsReadOnly();

    public static UserGroupChat CreateJoined(
        ChatterId chatterId,
        GroupChatId groupChatId, 
        DateTimeOffset joinedAt)
    {
        return new UserGroupChat(chatterId, groupChatId, joinedAt);
    }

    public void Join(DateTimeOffset joinedAt)
    {
        CheckRule(new GroupChatMustNotBeAlreadyJoinedRule(IsJoined, _joinLeaveTimeline));

        IsJoined = true;
        _joinLeaveTimeline.Add(joinedAt);
    }

    public void Leave(DateTimeOffset leftAt)
    {
        CheckRule(new GroupChatMustNotBeAlreadyLeftRule(IsJoined, _joinLeaveTimeline));

        IsJoined = false;
        _joinLeaveTimeline.Add(leftAt);
    }
}
