using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments.Events;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments.Rules;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments;

public sealed class SubscriptionPayment : AggregateRoot<SubscriptionPaymentId>
{
    public UserId PayerId { get; private set; } = default!;
    public AppSubscriptionPlanId AppSubscriptionPlanId { get; private set; } = default!;
    public SubscriptionPaymentStatus PaymentStatus { get; private set; }
    public DateTimeOffset? PaidAt { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; } = default!;

    public User Payer { get; private set; } = default!;
    public AppSubscriptionPlan AppSubscriptionPlan { get; private set; } = default!;

    private SubscriptionPayment() {}
    private SubscriptionPayment(
        SubscriptionPaymentId id,
        UserId payerId,
        AppSubscriptionPlanId appSubscriptionPlanId,
        SubscriptionPaymentStatus paymentStatus,
        DateTimeOffset expiresAt) 
    {
        Id = id;
        PayerId = payerId;
        AppSubscriptionPlanId = appSubscriptionPlanId;
        PaymentStatus = paymentStatus;
        ExpiresAt = expiresAt;
        this.CreatedBy = payerId.ToString();
        this.LastModifiedBy = payerId.ToString();
    }

    public static SubscriptionPayment CreatePaymentIntent(
        UserId payerId,
        AppSubscriptionPlanId appSubscriptionPlanId,
        DateTimeOffset expiresAt)
    {
        return new SubscriptionPayment(
            SubscriptionPaymentId.New(),
            payerId,
            appSubscriptionPlanId,
            SubscriptionPaymentStatus.PaymentIntentRequested,
            expiresAt);
    }

    public void MarkAsPaid(
        SubscriptionId subscriptionId,
        DateTimeOffset paidAt,
        DateTimeOffset subscriptionActivatedAt,
        DateTimeOffset subscriptionExpiresAt)
    {
        CheckRule(new PaymentIntentShouldBePaidBeforeExpirationRule(this));
        CheckRule(new PaymentIntentShouldBePaidOnlyOnceRule(this));

        this.PaymentStatus = SubscriptionPaymentStatus.Paid;
        this.PaidAt = paidAt;

        this.AddDomainEvent(new SubscriptionPaymentPaidDomainEvent(
            this.Id,
            subscriptionId,
            this.PayerId,
            this.AppSubscriptionPlanId,
            this.PaidAt.Value,
            subscriptionActivatedAt,
            subscriptionExpiresAt));
    }
}
