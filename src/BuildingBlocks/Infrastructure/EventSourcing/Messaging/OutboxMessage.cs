using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing.Messaging;

public class OutboxMessage : AggregateRoot
{
    public IDomainEventNotification Notification { get; set; } = default!;
    public string? Error { get; set; }
    public bool Processed { get; set; }
    public DateTimeOffset OccurredOn { get; set; }
    public DateTimeOffset? ProcessedDate { get; set; }

    private OutboxMessage() { }

    public static OutboxMessage Create<TNotification>(TNotification notification)
        where TNotification : IDomainEventNotification
    {
        return new OutboxMessage
        {
            Id = notification.Id,
            Notification = notification,
            OccurredOn = notification.Event.OccurredOn,
            Processed = false,
            ProcessedDate = null,
            Error = null
        };
    }
}
