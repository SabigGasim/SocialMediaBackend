using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.ProcessSubscriptionCreated;

[method: System.Text.Json.Serialization.JsonConstructor]
[method: Newtonsoft.Json.JsonConstructor]

public class ProcessSubscriptionCreatedCommand(
    Guid internalSubscriptionId,
    string gatewaySubscriptionId,
    string eventId,
    Guid id = default) : InternalCommandBase(id)
{
    public Guid InternalSubscriptionId { get; } = internalSubscriptionId;
    public string GatewaySubscriptionId { get; } = gatewaySubscriptionId;
    public string EventId { get; } = eventId;
}
