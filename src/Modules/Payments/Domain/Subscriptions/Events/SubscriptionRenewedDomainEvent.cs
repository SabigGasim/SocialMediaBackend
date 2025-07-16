using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Payers;

namespace SocialMediaBackend.Modules.Payments.Domain.Subscriptions.Events;

public sealed class SubscriptionRenewedDomainEvent(
    PayerId payerId,
    SubscriptionId subscriptionId,
    string productReference,
    DateTimeOffset renewedAt,
    DateTimeOffset expiresAt) : DomainEventBase
{
    public PayerId PayerId { get; } = payerId;
    public SubscriptionId SubscriptionId { get; } = subscriptionId;
    public string ProductReference { get; } = productReference;
    public DateTimeOffset RenewedAt { get; } = renewedAt;
    public DateTimeOffset ExpiresAt { get; } = expiresAt;
}