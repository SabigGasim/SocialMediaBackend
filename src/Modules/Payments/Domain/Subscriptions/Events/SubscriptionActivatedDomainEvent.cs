using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Payers;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

public class SubscriptionActivatedDomainEvent(
    PayerId payerId,
    SubscriptionId subscriptionId,
    string productReference,
    DateTimeOffset ActivatedAt,
    DateTimeOffset ExpiresAt) : DomainEventBase
{
    public PayerId PayerId { get; } = payerId;
    public SubscriptionId SubscriptionId { get; } = subscriptionId;
    public string ProductReference { get; } = productReference;
    public DateTimeOffset ActivatedAt { get; } = ActivatedAt;
    public DateTimeOffset ExpiresAt { get; } = ExpiresAt;
}
