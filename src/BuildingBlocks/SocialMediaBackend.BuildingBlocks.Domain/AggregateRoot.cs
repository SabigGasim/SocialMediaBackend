namespace SocialMediaBackend.BuildingBlocks.Domain;

public abstract class AggregateRoot<TId> : AuditableEntity<TId>, IHasDomainEvents
{
    private List<IDomainEvent>? _events;

    protected AggregateRoot() : base() {}

    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _events?.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _events ??= new();
        _events.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _events?.Clear();
    }
}
