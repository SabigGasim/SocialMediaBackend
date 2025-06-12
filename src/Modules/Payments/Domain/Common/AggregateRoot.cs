using Newtonsoft.Json;
using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Common;

public abstract class AggregateRoot : IStreamAggregate
{
    private List<IDomainEvent>? _domainEvents;
    private readonly List<IStreamEvent> _events = new();

    public Guid Id { get; protected set; }
    [JsonIgnore]
    public IReadOnlyCollection<IStreamEvent> Events => _events.AsReadOnly();
    [JsonIgnore]
    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= new();
        _domainEvents.Add(domainEvent);
    }
    public void ClearDomainEvents() => _domainEvents?.Clear();

    protected void AddEvent(IStreamEvent streamEvent) => _events.Add(streamEvent);
}
