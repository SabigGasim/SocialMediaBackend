using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;
internal class GroupChatMustNotBeAlreadyLeftRule(
    bool isJoined,
    List<DateTimeOffset> joinLeaveTimeline) : IBusinessRule
{
    private readonly bool _isJoined = isJoined;
    private readonly List<DateTimeOffset> _joinLeaveTimeline = joinLeaveTimeline;

    public string Message { get; } = "User already left the group or hasn't joined yet";

    public bool IsBroken()
    {
        return !_isJoined || _joinLeaveTimeline.Count == 0 || _joinLeaveTimeline.Count % 2 == 0;
    }

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
