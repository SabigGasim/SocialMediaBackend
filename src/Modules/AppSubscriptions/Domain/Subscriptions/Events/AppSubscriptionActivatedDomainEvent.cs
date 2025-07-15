using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Contracts;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions.Events;

public class AppSubscriptionActivatedDomainEvent(
    SubscriptionId subscriptionId,
    UserId subscriberId,
    AppSubscriptionTier tier,
    DateTimeOffset activatedAt,
    DateTimeOffset expiresAt) : DomainEventBase
{
    public SubscriptionId SubscriptionId { get; } = subscriptionId;
    public UserId SubscriberId { get; } = subscriberId;
    public AppSubscriptionTier SubscriptionTier { get; } = tier;
    public DateTimeOffset ActivatedAt { get; } = activatedAt;
    public DateTimeOffset ExpiresAt { get; } = expiresAt;
}
