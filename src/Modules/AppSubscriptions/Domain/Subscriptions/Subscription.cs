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

    public void Renew(DateTimeOffset renewedAt, DateTimeOffset expiresAt)
    {
        this.ActivatedAt = renewedAt;
        this.ExpiresAt = expiresAt;
        this.Status = SubscriptionStatus.Active;

        this.AddDomainEvent(new AppSubscriptionRenewedDomainEvent(
            this.Id,
            this.SubscriberId,
            this.ActivatedAt,
            this.ExpiresAt
            ));
    }

    public Result CancelAtPeriodEnd(DateTimeOffset requestedCancellationAt)
    {
        if (this.Status != SubscriptionStatus.Active)
        {
            return Result.FailureWithMessage(FailureCode.Conflict, "Subscription is not active, can't cancel at period end");
        }

        this.Status = SubscriptionStatus.CancleAtPeriodEnd;
        this.CanceledAt = requestedCancellationAt;
        this.AddDomainEvent(new AppSubscriptionAssignedToBeCanceledAtPeriodEndDomainEvent(this.Id));
        
        return Result.Success();
    }

    public Result Reactivate()
    {
        if (this.Status != SubscriptionStatus.CancleAtPeriodEnd)
        {
            return Result.FailureWithMessage(FailureCode.Conflict, "Can't reactivate an active/permenantly canceled subscription");
        }

        this.Status = SubscriptionStatus.Active;
        this.CanceledAt = null;

        return Result.Success();
    }

    public void Cancel(DateTimeOffset canceledAt)
    {
        this.Status = SubscriptionStatus.Canceled;
        this.CanceledAt = canceledAt;
        this.AddDomainEvent(new AppSubscriptionCanceledDomainEvent(
            this.Id,
            this.SubscriberId,
            canceledAt
        ));
    }
}