using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public interface IDomainEventsDispatcher
{
    ValueTask DispatchAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken ct = default)
        where TDomainEvent : IDomainEvent;

    ValueTask DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default);
}
