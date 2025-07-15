using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;

public sealed record SubscriptionId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static SubscriptionId New() => new(Guid.NewGuid());
}