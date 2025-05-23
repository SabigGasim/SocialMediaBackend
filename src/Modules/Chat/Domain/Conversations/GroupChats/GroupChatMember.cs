using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

public enum Membership
{
    Member = 0,
    Moderator = 1,
    Owner = 2
}

public class GroupChatMember
{
    private GroupChatMember() { }

    private GroupChatMember(
        GroupChatId groupChatId,
        ChatterId memberId, 
        DateTimeOffset memberSince,
        Membership membership)
    {
        GroupChatId = groupChatId;
        MemberId = memberId;
        MemberSince = memberSince;
        Membership = membership;
    }

    public GroupChatId GroupChatId { get; private set; } = default!;
    public ChatterId MemberId { get; private set; } = default!;
    public DateTimeOffset MemberSince { get; private set; }
    public Membership Membership { get; private set; }

    public GroupChat GroupChat { get; private set; } = default!;
    public Chatter Member { get; private set; } = default!;

    public static GroupChatMember Create(
        GroupChatId groupChatId, 
        ChatterId memberId,
        DateTimeOffset memberSince,
        Membership membership = Membership.Member)
    {
        return new GroupChatMember(groupChatId, memberId, memberSince, membership);
    }

    internal void UpdateMembership(Membership membership)
    {
        Membership = membership;
    }
}
