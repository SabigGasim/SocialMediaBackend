using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments;

public record SubscriptionPaymentId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static SubscriptionPaymentId New() => new(Guid.CreateVersion7());
}
