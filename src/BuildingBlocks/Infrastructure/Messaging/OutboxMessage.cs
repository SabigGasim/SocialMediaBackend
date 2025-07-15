using Newtonsoft.Json;
using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = default!;
    public string Content { get; set; } = default!;
    public string? Error { get; set; }
    public bool Processed { get; set; }
    public DateTimeOffset OccurredOn { get; set; }
    public DateTimeOffset? ProcessedDate { get; set; }

    public static OutboxMessage Create(IDomainEventNotification notification) 
    {
        return new OutboxMessage
        {
            Id = notification.Id,
            Content = JsonConvert.SerializeObject(notification),
            Type = notification.GetType().AssemblyQualifiedName!,
            OccurredOn = notification.Event.OccurredOn
        };
    }
}
