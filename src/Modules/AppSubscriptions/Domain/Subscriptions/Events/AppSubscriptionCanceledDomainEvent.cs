using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions.Events;

public sealed class AppSubscriptionCanceledDomainEvent(
    SubscriptionId subscriptionId,
    UserId subscriberId,
    DateTimeOffset canceledAt) : DomainEventBase
{
    public SubscriptionId SubscriptionId { get; } = subscriptionId;
    public UserId SubscriberId { get; } = subscriberId;
    public DateTimeOffset CanceledAt { get; } = canceledAt;
}
