using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.AppSubscriptions.Contracts.IntegrationEvents;

public sealed class AppSubscriptionActivatedIntegrationEvent(
    Guid subscriptionId,
    Guid subscriberId,
    AppSubscriptionTier subscriptionTier,
    DateTimeOffset activatedAt,
    DateTimeOffset expiresAt) : IntegrationEvent()
{
    public Guid SubscriptionId { get; } = subscriptionId;
    public Guid SubscriberId { get; } = subscriberId;
    public AppSubscriptionTier SubscriptionTier { get; } = subscriptionTier;
    public DateTimeOffset ActivatedAt { get; } = activatedAt;
    public DateTimeOffset ExpiresAt { get; } = expiresAt;
}
