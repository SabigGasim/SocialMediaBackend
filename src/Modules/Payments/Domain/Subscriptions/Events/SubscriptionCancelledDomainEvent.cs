using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Payers;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

public class SubscriptionCancelledDomainEvent(
    PayerId payerId,
    SubscriptionId subscriptionId,
    string productReference) : DomainEventBase
{
    public PayerId PayerId { get; } = payerId;
    public SubscriptionId SubscriptionId { get; } = subscriptionId;
    public string ProductReference { get; } = productReference;
}
