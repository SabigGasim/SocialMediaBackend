using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

public record SubscriptionId(Guid Id) : TypedIdValueBase<Guid>(Id)
{
    public static SubscriptionId New() => new(Guid.NewGuid());
}
