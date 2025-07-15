using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Contracts;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions.Events;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;

public sealed class Subscription : AggregateRoot<SubscriptionId>
{
    public UserId SubscriberId { get; private set; } = default!;
    public SubscriptionStatus Status { get; private set; }
    public AppSubscriptionTier Tier { get; private set; }
    public DateTimeOffset ActivatedAt { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }
    public DateTimeOffset? CanceledAt { get; private set; } = null;

    public User Subscriber { get; private set; } = default!;

    private Subscription() {}
    private Subscription(
        SubscriptionId subscriptionId, 
        UserId subscriberId, 
        SubscriptionStatus status,
        AppSubscriptionTier subscriptionTier,
        DateTimeOffset activatedAt,
        DateTimeOffset expiresAt)
    {
        this.Id = subscriptionId;
        this.SubscriberId = subscriberId;
        this.Status = status;
        this.Tier = subscriptionTier;
        this.ActivatedAt = activatedAt;
        this.ExpiresAt = expiresAt;
    }

    public static Subscription CreateActive(
        SubscriptionId subscriptionId, 
        UserId subscriberId,
        AppSubscriptionTier subscriptionTier,
        DateTimeOffset activatedAt,
        DateTimeOffset expiresAt)
    {
        var subscription = new Subscription(
            subscriptionId,
            subscriberId,
            SubscriptionStatus.Active,
            subscriptionTier,
            activatedAt,
            expiresAt);

        subscription.AddDomainEvent(new AppSubscriptionActivatedDomainEvent(
            subscriptionId,
            subscriberId,
            subscriptionTier,
            activatedAt,
            expiresAt));

        return subscription;
    }

    public void Cancel(DateTimeOffset canceledAt)
    {
        this.Status = SubscriptionStatus.Canceled;
        this.CanceledAt = canceledAt;
    }
}