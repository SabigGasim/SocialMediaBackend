using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Infrastructure.Processing;
public interface IDomainEventsDispatcher
{
    ValueTask DispatchAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken ct = default)
        where TDomainEvent : IDomainEvent;

    ValueTask DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default);
}
