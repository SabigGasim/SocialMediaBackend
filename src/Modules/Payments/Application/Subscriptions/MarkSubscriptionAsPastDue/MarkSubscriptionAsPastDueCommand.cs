using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.MarkSubscriptionAsPastDue;

public class MarkSubscriptionAsPastDueCommand(
    Guid internalSubscriptionId, 
    string eventId, 
    Guid id = default)
    : InternalCommandBase(id)
{
    public Guid InternalSubscriptionId { get; } = internalSubscriptionId;
    public string EventId { get; } = eventId;
}
