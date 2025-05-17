using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;

internal class GroupChatShouldHaveAtLeastTwoChattersRule(IEnumerable<Chatter> chatters) : IBusinessRule
{
    private readonly IEnumerable<Chatter> _chatters = chatters;

    public string Message => "Group chat should have at least two chatters";

    public bool IsBroken()
    {
        return _chatters.Count() < 2;
    }

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
