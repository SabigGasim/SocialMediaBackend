using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

public class SubscriptionCancelledIntegrationEvent(
    Guid payerId,
    Guid subscriptionId, 
    string productReference,
    DateTimeOffset canceledAt) : IntegrationEvent()
{
    public Guid PayerId { get; } = payerId;
    public Guid SubscriptionId { get; } = subscriptionId;
    public string ProductReference { get; } = productReference;
    public DateTimeOffset CanceledAt { get; } = canceledAt;
}
