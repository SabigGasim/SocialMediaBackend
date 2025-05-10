using Mediator;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

public interface IIntegrationEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IntegrationEvent;
