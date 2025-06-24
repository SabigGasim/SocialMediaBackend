using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

public record SubscriptionId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static SubscriptionId New() => new(Guid.NewGuid());
}
