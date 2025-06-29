using Newtonsoft.Json;

namespace SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

public abstract class AggregateRoot : IStreamAggregate
{
    private readonly List<IDomainEvent> _domainEvents = [];
    private readonly List<IStreamEvent> _streamEvents = [];

    public Guid Id { get; protected set; }
    [JsonIgnore]
    public IReadOnlyCollection<IStreamEvent> UnCommittedEvents => _streamEvents.AsReadOnly();
    [JsonIgnore]
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    protected void AddEvent(IStreamEvent streamEvent) => _streamEvents.Add(streamEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
    public void ClearStreamEvents() => _streamEvents.Clear();
}
