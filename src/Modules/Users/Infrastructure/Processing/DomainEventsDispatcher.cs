using Mediator;
using SocialMediaBackend.Modules.Users.Domain.Common;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Processing;

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
