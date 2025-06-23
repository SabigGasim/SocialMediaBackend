using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

public class SubscriptionCancelledDomainEvent(Guid subscriptionId, string productReference) : DomainEventBase
{
    public Guid SubscriptionId { get; } = subscriptionId;
    public string ProductReference { get; } = productReference;
}
