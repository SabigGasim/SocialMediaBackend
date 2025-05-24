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

    public GroupChatMember? Join(ChatterId memberId)
    {
        if (_members.Any(x => x.MemberId == memberId))
        {
            return null;
        }

        var member = GroupChatMember.Create(this.Id, memberId, DateTimeOffset.UtcNow);

        _members.Add(member);

        this.AddDomainEvent(new GroupChatJoinedDomainEvent(this.Id, memberId));

        return member;
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

    public Result KickMember(ChatterId kickerId, ChatterId targetId)
    {
        var kicker = _members.Find(x => x.MemberId == kickerId);
        var target = _members.Find(x => x.MemberId == targetId);

        if (kicker is null || target is null)
        {
            return Result.Failure(FailureCode.NotFound, "Kicker", "Target");
        }

        CheckRule(new ModeratorsCantKickThemselvesRule(kicker, target));
        CheckRule(new ModeratorMembershipMustBeHigherThanTheMemberToKick(kicker, target));

        _members.Remove(target);

        this.AddDomainEvent(new GroupMemberKickedDomainEvent(kicker.MemberId));

        return Result.Success();
    }

    public bool Leave(ChatterId memberId)
    {
        var member = _members.Find(x => x.MemberId == memberId);
        if (member is null)
        {
            return false;
        }

        _members.Remove(member);

        this.AddDomainEvent(new GroupChatLeftDomainEvent(this.Id, memberId));

        return true;
    }

    public Result PromoteMember(ChatterId promoterId, ChatterId targetId, Membership membership)
    {
        var promoter = _members.Find(x => x.MemberId == promoterId);
        var target = _members.Find(x => x.MemberId == targetId);

        if (promoter is null || target is null)
        {
            return Result.Failure(FailureCode.NotFound, "Target", "promoter");
        }

        CheckRule(new MembersCantPromoteThemselvesRule(promoter, target));
        CheckRule(new PromoterMembershipMustBeHigherThanTargetRule(promoter, target));
        CheckRule(new TargettedMembershipMustNotExceedPromoterMembershipRule(promoter, membership));

        if (membership == Membership.Owner)
        {
            promoter.UpdateMembership(Membership.Moderator);
        }

        target.UpdateMembership(membership);

        return Result.Success();
    }
}
