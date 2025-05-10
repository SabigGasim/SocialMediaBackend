using Mediator;

namespace SocialMediaBackend.BuildingBlocks.Domain;

public interface IDomainEventNotification : INotification
{
    Guid Id { get; }
}

public interface IDomainEventNotification<TEvent> : IDomainEventNotification
{
    TEvent Event { get; }
}
