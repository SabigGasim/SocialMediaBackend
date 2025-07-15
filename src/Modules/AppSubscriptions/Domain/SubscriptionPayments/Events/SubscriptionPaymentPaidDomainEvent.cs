using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments.Events;

public sealed class SubscriptionPaymentPaidDomainEvent(
    SubscriptionPaymentId subscriptionPaymentId,
    SubscriptionId subscriptionId,
    UserId payerId,
    AppSubscriptionPlanId appSubscriptionPlanId,
    DateTimeOffset paidAt,
    DateTimeOffset subscriptionActivatedAt,
    DateTimeOffset subscriptionExpiresAt) : DomainEventBase
{
    public SubscriptionPaymentId SubscriptionPaymentId { get; } = subscriptionPaymentId;
    public SubscriptionId SubscriptionId { get; } = subscriptionId;
    public UserId PayerId { get; } = payerId;
    public AppSubscriptionPlanId AppSubscriptionPlanId { get; } = appSubscriptionPlanId;
    public DateTimeOffset PaidAt { get; } = paidAt;
    public DateTimeOffset SubscriptionActivatedAt { get; } = subscriptionActivatedAt;
    public DateTimeOffset SubscriptionExpiresAt { get; } = subscriptionExpiresAt;
}
