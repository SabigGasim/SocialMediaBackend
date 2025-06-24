using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.RenewSubscription;

public class RenewSubscriptionCommand(
    Guid internalSubscriptionId,
    DateTimeOffset startDate,
    DateTimeOffset expirationDate,
    string eventId,
    Guid id = default) : InternalCommandBase(id)
{
    public Guid InternalSubscriptionId { get; } = internalSubscriptionId;
    public DateTimeOffset StartDate { get; } = startDate;
    public DateTimeOffset ExpirationDate { get; } = expirationDate;
    public string EventId { get; } = eventId;
}
