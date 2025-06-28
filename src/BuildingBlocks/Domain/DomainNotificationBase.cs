using Newtonsoft.Json;

namespace SocialMediaBackend.BuildingBlocks.Domain;

[method: JsonConstructor]
public abstract class DomainNotificationBase<T>(T domainEvent, Guid id) : IDomainEventNotification<T>
    where T : IDomainEvent
{
    [JsonProperty("domainEvent")]
    public T Event { get; } = domainEvent;
    public Guid Id { get; } = id;

    [JsonIgnore]
    IDomainEvent IDomainEventNotification.Event => Event;
}
