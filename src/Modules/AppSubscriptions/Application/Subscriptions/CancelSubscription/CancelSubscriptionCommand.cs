using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.CancelSubscription;

[method: System.Text.Json.Serialization.JsonConstructor]
[method: Newtonsoft.Json.JsonConstructor]
public sealed class CancelSubscriptionCommand(
    Guid id,
    SubscriptionId subscriptionId,
    DateTimeOffset canceledAt) : InternalCommandBase(id)
{
    public SubscriptionId SubscriptionId { get; } = subscriptionId;
    public DateTimeOffset CanceledAt { get; } = canceledAt;
}
