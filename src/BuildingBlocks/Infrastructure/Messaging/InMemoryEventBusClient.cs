using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;

public class InMemoryEventBusClient : IEventBus
{
    public async Task Publish<TEvent>(TEvent @event) where TEvent : IntegrationEvent
    {
        await InMemoryEventBus.Instance.Publish(@event);
    }

    public void Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IntegrationEvent
    {
        InMemoryEventBus.Instance.Subscribe(handler);
    }
}
