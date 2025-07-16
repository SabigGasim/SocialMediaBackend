using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions.Events;
public sealed class AppSubscriptionRenewedDomainEvent(
    SubscriptionId subscriptionId,
    UserId subscriberId,
    DateTimeOffset renewedAt,
    DateTimeOffset expiresAt) : DomainEventBase
{
    public SubscriptionId SubscriptionId { get; } = subscriptionId;
    public UserId SubscriberId { get; } = subscriberId;
    public DateTimeOffset RenewedAt { get; } = renewedAt;
    public DateTimeOffset ExpiresAt { get; } = expiresAt;
}