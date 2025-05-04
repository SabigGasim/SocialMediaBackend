using SocialMediaBackend.Modules.Users.Domain.Common;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Processing;
public interface IDomainEventsDispatcher
{
    ValueTask DispatchAsync<TDomainEvent>(TDomainEvent domainEvent, CancellationToken ct = default)
        where TDomainEvent : IDomainEvent;

    ValueTask DispatchAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken ct = default);
}
