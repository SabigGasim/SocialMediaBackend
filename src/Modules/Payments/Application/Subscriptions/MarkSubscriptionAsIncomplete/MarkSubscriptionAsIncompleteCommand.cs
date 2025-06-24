using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.MarkSubscriptionAsIncomplete;

public class MarkSubscriptionAsIncompleteCommand(
    Guid internalSubscriptionId,
    string eventId,
    Guid id = default)
    : InternalCommandBase(id)
{
    public Guid InternalSubscriptionId { get; } = internalSubscriptionId;
    public string EventId { get; } = eventId;
}
