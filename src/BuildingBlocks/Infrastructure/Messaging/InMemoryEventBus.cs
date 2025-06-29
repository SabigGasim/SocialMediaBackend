using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Helpers;
using System.Collections.Concurrent;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;

internal class InMemoryEventBus : IEventBus
{
    private readonly Dictionary<string, List<Func<IntegrationEvent, ValueTask>>> _handlerFunctions = [];
    private readonly ConcurrentQueue<(string, IntegrationEvent)> _events = [];
    private readonly TimeSpan _interval = TimeSpan.FromSeconds(2);

    private InMemoryEventBus() { }

    public static InMemoryEventBus Instance { get; } = new InMemoryEventBus();

    public Task Publish<TEvent>(TEvent @event) where TEvent : IntegrationEvent
    {
        this.EnqueueEvent(@event);

        return Task.CompletedTask;
    }

    public void Subscribe<TEvent>(IIntegrationEventHandler<TEvent> handler) where TEvent : IntegrationEvent
    {
        var eventName = typeof(TEvent).FullName!;

        if (!_handlerFunctions.TryGetValue(eventName, out _))
        {
            _handlerFunctions.Add(eventName, new(1));
        }

        _handlerFunctions[eventName].Add(@event =>
        {
            return handler.Handle((TEvent)@event, CancellationToken.None);
        });
    }

    private void EnqueueEvent<TEvent>(TEvent @event) where TEvent : IntegrationEvent
    {
        var eventName = typeof(TEvent).FullName!;

        _events.Enqueue((eventName, @event));
    }

    public async Task PublishQueuedEvents(CancellationToken ct)
    {
        await Task.Delay(_interval, ct);

        while (_events.TryDequeue(out (string, IntegrationEvent) item))
        {
            var (name, @event) = item;

            if (_handlerFunctions.TryGetValue(name, out var handlerFunction))
            {
                await handlerFunction
                    .Select(async handler => await handler(@event))
                    .ProcessIndividually(DescribePublishFailure(name, @event));
            }
        }
    }

    private static string DescribePublishFailure(string name, IntegrationEvent @event)
    {
        return $"[{@event.OccurredOn}] Couldn't send event {name} with Id ({@event.Id})";
    }
}
