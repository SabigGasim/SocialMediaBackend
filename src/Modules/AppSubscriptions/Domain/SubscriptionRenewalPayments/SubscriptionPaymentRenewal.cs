using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionRenewalPayments.Events;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionRenewalPayments.Rules;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionRenewalPayments;

public sealed class SubscriptionRenewalPayment : AggregateRoot<SubscriptionRenewalPaymentId>
{
    public UserId PayerId { get; private set; } = default!;
    public SubscriptionId SubscriptionId { get; private set; } = default!;
    public SubscriptionPaymentStatus PaymentStatus { get; private set; }
    public DateTimeOffset? PaidAt { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; } = default!;

    public User Payer { get; private set; } = default!;
    public Subscription Subscription { get; private set; } = default!;

    private SubscriptionRenewalPayment() {}
    private SubscriptionRenewalPayment(
        SubscriptionRenewalPaymentId id,
        SubscriptionId subscriptionId,
        UserId payerId,
        SubscriptionPaymentStatus paymentStatus,
        DateTimeOffset expiresAt) 
    {
        Id = id;
        SubscriptionId = subscriptionId;
        PayerId = payerId;
        PaymentStatus = paymentStatus;
        ExpiresAt = expiresAt;
        this.CreatedBy = payerId.Value.ToString();
        this.LastModifiedBy = payerId.Value.ToString();
    }

    public static SubscriptionRenewalPayment CreatePaymentIntent(
        SubscriptionId subscriptionId,
        UserId payerId, 
        DateTimeOffset expiresAt)
    {
        return new SubscriptionRenewalPayment(
            SubscriptionRenewalPaymentId.New(),
            subscriptionId,
            payerId,
            SubscriptionPaymentStatus.PaymentIntentRequested,
            expiresAt);
    }

    public void MarkAsPaid(
        DateTimeOffset paidAt,
        DateTimeOffset subscriptionActivatedAt,
        DateTimeOffset subscriptionExpiresAt)
    {
        CheckRule(new PaymentIntentShouldBePaidBeforeExpirationRule(this));
        CheckRule(new PaymentIntentShouldBePaidOnlyOnceRule(this));

        this.PaymentStatus = SubscriptionPaymentStatus.Paid;
        this.PaidAt = paidAt;

        this.AddDomainEvent(new SubscriptionRenewalPaymentPaidDomainEvent(
            this.Id,
            this.SubscriptionId,
            this.PayerId,
            this.PaidAt.Value,
            subscriptionActivatedAt,
            subscriptionExpiresAt));
    }
}
