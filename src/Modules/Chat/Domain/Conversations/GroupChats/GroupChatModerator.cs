using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

public class GroupChatModerator
{
    private GroupChatModerator() { }

    private GroupChatModerator(GroupChatId groupChatId, ChatterId moderatorId, DateTimeOffset assignedAt)
    {
        GroupChatId = groupChatId;
        ModeratorId = moderatorId;
        AssignedAt = assignedAt;
    }

    public GroupChatId GroupChatId { get; private set; } = default!;
    public ChatterId ModeratorId { get; private set; } = default!;
    public DateTimeOffset AssignedAt { get; private set; }

    public GroupChat GroupChat { get; private set; } = default!;
    public Chatter Moderator { get; private set; } = default!;

    public static GroupChatModerator Create(GroupChatId groupChatId, ChatterId moderatorId)
    {
        return new GroupChatModerator(groupChatId, moderatorId, DateTimeOffset.UtcNow);
    }
}
