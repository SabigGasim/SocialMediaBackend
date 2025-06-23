using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.CancelSubscription;

public class CancelSubscriptionCommand(Guid internalSubscriptionId, string eventJson, Guid id = default)
    : InternalCommandBase(id)
{
    public Guid InternalSubscriptionId { get; } = internalSubscriptionId;
    public string EventJson { get; } = eventJson;
}
