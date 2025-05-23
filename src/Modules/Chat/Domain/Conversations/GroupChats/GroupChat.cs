using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Events;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

public class GroupChat : AggregateRoot<GroupChatId>
{
    private readonly List<GroupChatMember> _members = new();
    private readonly List<GroupMessage> _messages = new();

    private GroupChat() { }

    private GroupChat(
        ChatterId creatorId,
        string name,
        IEnumerable<ChatterId> membersIds)
    {
        Id = GroupChatId.New();
        Name = name;
        
        var now = DateTimeOffset.UtcNow;

        _members.AddRange(membersIds.Select(x => GroupChatMember.Create(this.Id, x, DateTimeOffset.UtcNow)));

        if (!membersIds.Contains(creatorId))
        {
            _members.Add(GroupChatMember.Create(this.Id, creatorId, DateTimeOffset.UtcNow, Membership.Owner));
        }

        Created = now;
        CreatedBy = creatorId.ToString();
        LastModified = now;
        LastModifiedBy = creatorId.ToString();

        this.AddDomainEvent(new GroupChatCreatedDomainEvent(this.Id, now, membersIds));
    }

    public string Name { get; private set; } = default!;
    public IReadOnlyCollection<GroupChatMember> Members => _members.AsReadOnly();
    public IReadOnlyCollection<GroupMessage> Messages => _messages.AsReadOnly();

    public static GroupChat Create(
        ChatterId creatorId,
        string name,
        IEnumerable<ChatterId> memberIds)
    {
        CheckRule(new GroupChatShouldHaveAtLeastTwoMembersRule(memberIds));

        return new GroupChat(creatorId, name, memberIds);
    }

    public GroupMessage AddMessage(ChatterId senderId, string text)
    {
        var message = GroupMessage.Create(senderId, this.Id, text, DateTimeOffset.UtcNow);

        _messages.Add(message);

        this.AddDomainEvent(new GroupMessageCreatedDomainEvent(this.Id, message.Id));

        return message;
    }

    public bool DeleteMessage(GroupMessageId messageId)
    {
        var message = _messages.Find(x => x.Id == messageId);
        if (message is null)
        {
            return false;
        }

        CheckRule(new MessageMustBeSentAtMostFourHoursAgoToBeDeletedForEveryoneRule(message.SentAt));

        _messages.Remove(message);

        this.AddDomainEvent(new GroupMessageDeletedDomainEvent(messageId));

        return true;
    }

    public void RemoveMember(GroupChatMember kickerMember, GroupChatMember memberToKick)
    {
        CheckRule(new ModeratorsCantKickThemselvesRule(kickerMember, memberToKick));
        CheckRule(new ModeratorMembershipMustBeHigherThanTheMemberToKick(kickerMember, memberToKick));

        _members.Remove(memberToKick);

        this.AddDomainEvent(new GroupMemberKickedDomainEvent(memberToKick.MemberId));
    }
}
