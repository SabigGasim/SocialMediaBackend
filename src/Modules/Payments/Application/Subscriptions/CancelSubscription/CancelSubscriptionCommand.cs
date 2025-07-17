using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.CancelSubscription;

public class CancelSubscriptionCommand(
    Guid internalSubscriptionId, 
    string eventId,
    DateTimeOffset canceledAt,
    Guid id = default)
    : InternalCommandBase(id)
{
    public Guid InternalSubscriptionId { get; } = internalSubscriptionId;
    public string EventId { get; } = eventId;
    public DateTimeOffset CanceledAt { get; } = canceledAt;
}
