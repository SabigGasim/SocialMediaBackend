using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Chat.Domain.Conversations.GroupChats.Rules;
public class TargettedMembershipMustNotExceedPromoterMembershipRule(
    GroupChatMember promoter, 
    Membership membership) : IBusinessRule
{
    private readonly GroupChatMember _promoter = promoter;
    private readonly Membership _membership = membership;

    public string Message { get; } = "Cannot promote other members higher than your membership";

    public bool IsBroken()
    {
        return _promoter.Membership < _membership;
    }

    public Task<bool> IsBrokenAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
