using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;

internal class PromoterMembershipMustBeHigherThanTargetRule(GroupChatMember promoter, GroupChatMember target)
    : IBusinessRule
{
    private readonly GroupChatMember _promoter = promoter;
    private readonly GroupChatMember _target = target;

    public string Message { get; } = "Members can't promote other members with the same membership or higher";

    public bool IsBroken()
    {
        return _promoter.Membership <= _target.Membership;
    }

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
