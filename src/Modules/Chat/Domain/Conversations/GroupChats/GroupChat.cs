using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;
using SocialMediaBackend.Modules.Chat.Domain.Messages.GroupMessages;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats;

public class GroupChat : AggregateRoot<GroupChatId>
{
    private readonly List<Chatter> _chatters = new();
    private readonly List<Chatter>? _moderators;
    private readonly List<GroupMessage>? _messages;

    private GroupChat() { }

    private GroupChat(
        ChatterId creatorId,
        IEnumerable<Chatter> chatters,
        IEnumerable<Chatter>? moderators = null)
    {
        Id = GroupChatId.New();
        OwnerId = creatorId;
        _chatters.AddRange(chatters);
        if (moderators is not null && moderators.Any())
        {
            _moderators ??= new();
            _moderators.AddRange(moderators);
        }

        var now = DateTimeOffset.UtcNow;

        Created = now;
        CreatedBy = creatorId.ToString();
        LastModified = now;
        LastModifiedBy = creatorId.ToString();
    }

    public ChatterId OwnerId { get; private set; } = default!;
    public Chatter Owner { get; private set; } = default!;

    public IReadOnlyCollection<Chatter>? Moderators => _moderators?.AsReadOnly();
    public IReadOnlyCollection<Chatter> Chatters => _chatters.AsReadOnly();
    public IReadOnlyCollection<GroupMessage>? Messages => _messages?.AsReadOnly();

    public static GroupChat Create(
        ChatterId creatorId,
        IEnumerable<Chatter> chatters,
        IEnumerable<Chatter>? moderators = null)
    {
        CheckRule(new GroupChatShouldHaveAtLeastTwoChattersRule(chatters));

        return new GroupChat(creatorId, chatters, moderators);
    }
}
