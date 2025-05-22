using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Events;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;
using System.Collections.Generic;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

public class GroupChat : AggregateRoot<GroupChatId>
{
    private readonly List<GroupChatMember> _chatters = new();
    private readonly List<GroupChatModerator> _moderators = new();
    private readonly List<GroupMessage> _messages = new();

    private GroupChat() { }

    private GroupChat(
        ChatterId creatorId,
        string name,
        IEnumerable<ChatterId> membersIds,
        IEnumerable<ChatterId>? moderatorIds = null)
    {
        Id = GroupChatId.New();
        OwnerId = creatorId;
        Name = name;
        
        _chatters.AddRange(membersIds.Select(x => GroupChatMember.Create(this.Id, x)));
        if (moderatorIds is not null && moderatorIds.Any())
        {
            _moderators ??= new();
            _moderators.AddRange(moderatorIds.Select(x => GroupChatModerator.Create(this.Id, x)));
        }

        var now = DateTimeOffset.UtcNow;

        Created = now;
        CreatedBy = creatorId.ToString();
        LastModified = now;
        LastModifiedBy = creatorId.ToString();

        this.AddDomainEvent(new GroupChatCreatedDomainEvent(this.Id, now, membersIds));
    }

    public ChatterId OwnerId { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public DateTimeOffset CreatedAt { get; private set; }

    public Chatter Owner { get; private set; } = default!;
    public IReadOnlyCollection<GroupChatModerator> Moderators => _moderators.AsReadOnly();
    public IReadOnlyCollection<GroupChatMember> Members => _chatters.AsReadOnly();
    public IReadOnlyCollection<GroupMessage> Messages => _messages.AsReadOnly();

    public static GroupChat Create(
        ChatterId creatorId,
        string name,
        IEnumerable<ChatterId> memberIds,
        IEnumerable<ChatterId>? moderatorIds = null)
    {
        CheckRule(new GroupChatShouldHaveAtLeastTwoMembersRule(memberIds));

        return new GroupChat(creatorId, name, memberIds, moderatorIds);
    }

    public GroupMessage AddMessage(ChatterId senderId, string text)
    {
        var message = GroupMessage.Create(senderId, this.Id, text, DateTimeOffset.UtcNow);

        _messages.Add(message);

        this.AddDomainEvent(new GroupMessageCreatedDomainEvent(this.Id, message.Id));

        return message;
    }
}
