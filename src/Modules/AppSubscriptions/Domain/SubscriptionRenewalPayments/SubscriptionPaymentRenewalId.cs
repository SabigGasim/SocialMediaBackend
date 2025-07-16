using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionRenewalPayments;

public record SubscriptionRenewalPaymentId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static SubscriptionRenewalPaymentId New() => new(Guid.CreateVersion7());
}
