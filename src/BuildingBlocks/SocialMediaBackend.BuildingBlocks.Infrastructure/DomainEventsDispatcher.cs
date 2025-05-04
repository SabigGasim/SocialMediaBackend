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
        var tasks = domainEvents.Select(x => _mediator.Publish(x, ct));

        foreach (var task in tasks)
        {
            await task;
        }
    }
}
