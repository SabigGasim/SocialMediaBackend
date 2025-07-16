using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.AppSubscriptions.Contracts.IntegrationEvents;

public sealed class AppSubscriptionRenewedIntegrationEvent(
    Guid subscriptionId,
    Guid subscriberId,
    DateTimeOffset renewedAt,
    DateTimeOffset expiresAt) : IntegrationEvent()
{
    public Guid SubscriptionId { get; } = subscriptionId;
    public Guid SubscriberId { get; } = subscriberId;
    public DateTimeOffset RenewedAt { get; } = renewedAt;
    public DateTimeOffset ExpiresAt { get; } = expiresAt;
}
