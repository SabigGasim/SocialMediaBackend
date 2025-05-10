using Mediator;
using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public class DomainEventsDispatcher(IMediator mediator) : IDomainEventsDispatcher
{
    private readonly IMediator _mediator = mediator;

    public ValueTask DispatchAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken ct = default)
        where TDomainEvent : IDomainEvent
    {
        return _mediator.Publish(domainEvent, ct);
    }

    public async ValueTask DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default)
    {
        foreach (var @event in domainEvents)
        {
            await _mediator.Publish(@event, ct);
        }
    }
}
