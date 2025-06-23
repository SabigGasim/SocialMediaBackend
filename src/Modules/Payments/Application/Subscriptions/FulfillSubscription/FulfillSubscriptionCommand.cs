using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.FulfillSubscription;

[method: System.Text.Json.Serialization.JsonConstructor]
[method: Newtonsoft.Json.JsonConstructor]
public class FulfillSubscriptionCommand(
    Guid internalSubscriptionId,
    SubscriptionStatus status,
    DateTimeOffset startDate, 
    DateTimeOffset expirationDate,
    string eventJson,
    Guid id = default) : InternalCommandBase(id)
{
    public Guid InternalSubscriptionId { get; } = internalSubscriptionId;
    public SubscriptionStatus Status { get; } = status;
    public DateTimeOffset StartDate { get; } = startDate;
    public DateTimeOffset ExpirationDate { get; } = expirationDate;
    public string EventJson { get; } = eventJson;
}
