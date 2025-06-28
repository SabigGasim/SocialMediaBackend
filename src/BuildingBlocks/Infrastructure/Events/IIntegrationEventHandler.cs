namespace SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

public interface IIntegrationEventHandler<in TEvent> : IIntegrationEventHandler
    where TEvent : IntegrationEvent
{
    ValueTask Handle(TEvent @event, CancellationToken cancellationToken = default);
}


public interface IIntegrationEventHandler;
