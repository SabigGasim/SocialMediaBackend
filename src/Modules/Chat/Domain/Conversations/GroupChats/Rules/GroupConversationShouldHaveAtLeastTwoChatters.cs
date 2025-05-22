using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;

internal class GroupChatShouldHaveAtLeastTwoMembersRule(IEnumerable<ChatterId> members) : IBusinessRule
{
    private readonly IEnumerable<ChatterId> _members = members;

    public string Message => "Group chat should have at least two chatters";

    public bool IsBroken()
    {
        return _members.Count() < 2;
    }

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
