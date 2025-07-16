using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions.Events;

public sealed class AppSubscriptionAssignedToBeCanceledAtPeriodEndDomainEvent(SubscriptionId subscriptionId)
    : DomainEventBase
{
    public SubscriptionId SubscriptionId { get; } = subscriptionId;
}
