using Mediator;

namespace SocialMediaBackend.BuildingBlocks.Domain;

public interface IDomainEventNotification : INotification
{
    Guid Id { get; }
    IDomainEvent Event { get; }
}

public interface IDomainEventNotification<TEvent> : IDomainEventNotification
    where TEvent : IDomainEvent
{
    new TEvent Event { get; }
}
