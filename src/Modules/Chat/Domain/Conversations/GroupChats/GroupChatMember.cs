using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

public class GroupChatMember
{
    private GroupChatMember() { }

    private GroupChatMember(GroupChatId groupChatId, ChatterId memberId)
    {
        GroupChatId = groupChatId;
        MemberId = memberId;
    }

    public GroupChatId GroupChatId { get; private set; } = default!;
    public ChatterId MemberId { get; private set; } = default!;
    
    public GroupChat GroupChat { get; private set; } = default!;
    public Chatter Member { get; private set; } = default!;

    public static GroupChatMember Create(GroupChatId groupChatId, ChatterId memberId)
    {
        return new GroupChatMember(groupChatId, memberId);
    }
}
