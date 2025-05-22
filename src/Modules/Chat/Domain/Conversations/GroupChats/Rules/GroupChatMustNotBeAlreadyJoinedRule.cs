using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;

internal class GroupChatMustNotBeAlreadyJoinedRule(bool isJoined) : IBusinessRule
{
    private readonly bool _isJoined = isJoined;

    public string Message { get; } = "User already joined the group";

    public bool IsBroken()
    {
        return _isJoined;
    }

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
