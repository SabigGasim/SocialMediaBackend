using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;

public class ModeratorMembershipMustBeHigherThanTheMemberToKick(GroupChatMember moderator, GroupChatMember memberToKick) 
    : IBusinessRule
{
    private readonly GroupChatMember _moderator = moderator;
    private readonly GroupChatMember _memberToKick = memberToKick;

    public string Message { get; } = "Moderator membership must be higher than the member to kick";
    public bool IsBroken()
    {
        return _moderator.Membership <= _memberToKick.Membership;
    }

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
