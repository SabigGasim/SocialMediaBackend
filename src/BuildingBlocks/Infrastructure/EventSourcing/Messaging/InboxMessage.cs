using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing.Messaging;

public class InboxMessage : AggregateRoot
{
    public IntegrationEvent Notification { get; set; } = default!;
    public string? Error { get; set; }
    public bool Processed { get; set; }
    public DateTimeOffset OccurredOn { get; set; }
    public DateTimeOffset? ProcessedDate { get; set; }

    private InboxMessage() { }

    public static InboxMessage Create<TNotification>(TNotification notification)
        where TNotification : IntegrationEvent
    {
        return new InboxMessage
        {
            Id = notification.Id,
            Notification = notification,
            OccurredOn = notification.OccurredOn,
            Processed = false,
            ProcessedDate = null,
            Error = null
        };
    }
}
