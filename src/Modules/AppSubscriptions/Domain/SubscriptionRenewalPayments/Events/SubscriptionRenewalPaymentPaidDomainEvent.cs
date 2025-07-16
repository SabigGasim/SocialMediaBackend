using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionRenewalPayments.Events;

public sealed class SubscriptionRenewalPaymentPaidDomainEvent(
    SubscriptionRenewalPaymentId subscriptionRenewalPaymentId,
    SubscriptionId subscriptionId,
    UserId payerId,
    DateTimeOffset paidAt,
    DateTimeOffset subscriptionRenewedAt,
    DateTimeOffset subscriptionExpiresAt) : DomainEventBase
{
    public SubscriptionRenewalPaymentId SubscriptionRenewalPaymentId { get; } = subscriptionRenewalPaymentId;
    public SubscriptionId SubscriptionId { get; } = subscriptionId;
    public UserId PayerId { get; } = payerId;
    public DateTimeOffset PaidAt { get; } = paidAt;
    public DateTimeOffset SubscriptionRenewedAt { get; } = subscriptionRenewedAt;
    public DateTimeOffset SubscriptionExpiresAt { get; } = subscriptionExpiresAt;
}
