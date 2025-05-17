using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;

internal class GroupChatMustNotBeAlreadyJoinedRule(
    bool isJoined,
    List<DateTimeOffset> joinLeaveTimeline) : IBusinessRule
{
    private readonly bool _isJoined = isJoined;
    private readonly List<DateTimeOffset> _joinLeaveTimeline = joinLeaveTimeline;

    public string Message { get; } = "User already joined the group";

    public bool IsBroken()
    {
        return _isJoined || _joinLeaveTimeline.Count % 2 == 1;
    }

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
