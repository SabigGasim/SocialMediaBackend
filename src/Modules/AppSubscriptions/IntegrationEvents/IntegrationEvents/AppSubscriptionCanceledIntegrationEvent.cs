using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.AppSubscriptions.Contracts.IntegrationEvents;

public sealed class AppSubscriptionCanceledIntegrationEvent(
    Guid subscriptionId,
    Guid subscriberId,
    DateTimeOffset canceledAt) : IntegrationEvent()
{
    public Guid SubscriptionId { get; } = subscriptionId;
    public Guid SubscriberId { get; } = subscriberId;
    public DateTimeOffset CanceledAt { get; } = canceledAt;
}
