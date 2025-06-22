using Newtonsoft.Json;
using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Common;

public abstract class AggregateRoot : IStreamAggregate
{
    private List<IDomainEvent>? _domainEvents;
    private readonly List<IStreamEvent> _streamEvents = new();

    public Guid Id { get; protected set; }
    [JsonIgnore]
    public IReadOnlyCollection<IStreamEvent> UnCommittedEvents => _streamEvents.AsReadOnly();
    [JsonIgnore]
    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new();
        _domainEvents.Add(domainEvent);
    }
    protected void AddEvent(IStreamEvent streamEvent) => _streamEvents.Add(streamEvent);

    public void ClearDomainEvents() => _domainEvents?.Clear();
    public void ClearStreamEvents() => _streamEvents.Clear();
}
