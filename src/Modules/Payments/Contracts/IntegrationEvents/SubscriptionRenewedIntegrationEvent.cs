using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

public sealed class SubscriptionRenewedIntegrationEvent(
    Guid payerId,
    Guid subscriptionId,
    string productReference,
    DateTimeOffset renewedAT,
    DateTimeOffset expiresAt) : IntegrationEvent()
{
    public Guid PayerId { get; } = payerId;
    public Guid SubscriptionId { get; } = subscriptionId;
    public string ProductReference { get; } = productReference;
    public DateTimeOffset RenewedAt { get; } = renewedAT;
    public DateTimeOffset ExpiresAt { get; } = expiresAt;
}
