using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.FulfillSubscription;

[method: System.Text.Json.Serialization.JsonConstructor]
[method: Newtonsoft.Json.JsonConstructor]

public class FulfillSubscriptionCommand(
    Guid internalSubscriptionId,
    DateTimeOffset activatedAt,
    DateTimeOffset expiresAt,
    string eventId,
    Guid id = default) : InternalCommandBase(id)
{
    public Guid InternalSubscriptionId { get; } = internalSubscriptionId;
    public DateTimeOffset ActivatedAt { get; } = activatedAt;
    public DateTimeOffset ExpiresAt { get; } = expiresAt;
    public string EventId { get; } = eventId;
}
