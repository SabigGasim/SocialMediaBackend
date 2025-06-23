using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

public class SubscriptionActivatedIntegrationEvent(
    Guid payerId,
    Guid subscriptionId,
    string productReference,
    DateTimeOffset activatedAt,
    DateTimeOffset expiresAt) : IntegrationEvent()
{
    public Guid PayerId { get; } = payerId;
    public Guid SubscriptionId { get; } = subscriptionId;
    public string ProductReference { get; } = productReference;
    public DateTimeOffset ActivatedAt { get; } = activatedAt;
    public DateTimeOffset ExpiresAt { get; } = expiresAt;
}
