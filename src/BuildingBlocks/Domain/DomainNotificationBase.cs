
namespace SocialMediaBackend.BuildingBlocks.Domain;

public abstract class DomainNotificationBase : IDomainEventNotification
{
    public Guid Id { get; private set; }
}

public abstract class DomainNotificationBase<T> : IDomainEventNotification<T>
    where T : IDomainEvent
{
    protected DomainNotificationBase(T domainEvent, Guid id)
    {
        Event = domainEvent;
        Id = id;
    }

    public T Event { get; }
    public Guid Id { get; }
}
