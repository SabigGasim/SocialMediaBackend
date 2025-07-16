using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.RenewSubscription;

[method: System.Text.Json.Serialization.JsonConstructor]
[method: Newtonsoft.Json.JsonConstructor]
public sealed class CompleteSubscriptionRenewalPaymentCommand(
    Guid id,
    UserId subscriberId,
    SubscriptionId subscriptionId,
    DateTimeOffset paidAt,
    DateTimeOffset subscriptionActivatedAt,
    DateTimeOffset subscriptionExpiresAt) : InternalCommandBase(id)
{
    public UserId SubscriberId { get; } = subscriberId;
    public SubscriptionId SubscriptionId { get; } = subscriptionId;
    public DateTimeOffset PaidAt { get; } = paidAt;
    public DateTimeOffset SubscriptionActivatedAt { get; } = subscriptionActivatedAt;
    public DateTimeOffset SubscriptionExpiresAt { get; } = subscriptionExpiresAt;
}
