using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;

public class ModeratorsCantKickThemselvesRule(GroupChatMember moderator, GroupChatMember memberToKick) : IBusinessRule
{
    private readonly GroupChatMember _moderator = moderator;
    private readonly GroupChatMember _memberToKick = memberToKick;

    public string Message => throw new NotImplementedException();

    public bool IsBroken()
    {
        return _moderator.MemberId == _memberToKick.MemberId;
    }

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
