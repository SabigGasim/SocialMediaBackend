using Mediator;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Infrastructure.Helpers;

namespace SocialMediaBackend.Infrastructure.Processing;

public class DomainEventsDispatcher(IMediator mediator) : IDomainEventsDispatcher
{
    private readonly IMediator _mediator = mediator;

    public ValueTask DispatchAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken ct = default)
        where TDomainEvent : IDomainEvent
    {
        return _mediator.Publish(domainEvent, ct);
    }

    public ValueTask DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default)
    {
        var tasks = domainEvents.Select(x => _mediator.Publish(x, ct));

        return ValueTaskHelper.WhenAll(tasks);
    }
}
