using Mediator;
using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.BuildingBlocks.Application;
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent;
