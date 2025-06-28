using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;

public interface IEventBus
{
    void Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler)
        where TEvent : IntegrationEvent;

    Task Publish<TEvent>(TEvent @event) where TEvent : IntegrationEvent;
}
