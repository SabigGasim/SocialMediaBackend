using Autofac;
using Autofac.Core;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing.Messaging;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;

public sealed class MartenUnitOfWork(
    IAggregateRepository repository,
    IAggregateTracker aggregateTracker,
    IDomainEventsDispatcher dispatcher,
    ILifetimeScope scope) : IUnitOfWork
{
    private readonly IAggregateRepository _repository = repository;
    private readonly IAggregateTracker _aggregateTracker = aggregateTracker;
    private readonly IDomainEventsDispatcher _dispatcher = dispatcher;
    private readonly ILifetimeScope _scope = scope;

    public async Task<int> CommitAsync(CancellationToken ct = default)
    {
        var entitiesWithEvents = _aggregateTracker.GetTrackedAggregates();

        var domainEvents = entitiesWithEvents
            .Where(x => x.DomainEvents is { Count: > 0 })
            .SelectMany(e => e.DomainEvents!)
            .ToArray();

        List<IDomainEventNotification> domainEventNotifications = [];
        foreach (var domainEvent in domainEvents)
        {
            Type domainEvenNotificationType = typeof(IDomainEventNotification<>);

            var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
            var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
            {
                new NamedParameter("id", domainEvent.Id),
                new NamedParameter("domainEvent", domainEvent!)
            });

            if (domainNotification is not null)
            {
                var notification = (IDomainEventNotification)domainNotification;

                domainEventNotifications.Add(notification);
            }
        }

        foreach (var entity in entitiesWithEvents)
        {
            entity.ClearDomainEvents();
        }

        await _dispatcher.DispatchAsync(domainEvents, ct);

        var entitiesWithStreamEvents = entitiesWithEvents.Where(x => x.UnCommittedEvents is { Count: > 0 });

        foreach (var entity in entitiesWithStreamEvents)
        {
            _repository.AppendUnCommittedEvents(entity);
        }

        foreach (var notification in domainEventNotifications)
        {
            var outboxMessage = OutboxMessage.Create(notification);

            _repository.Store(outboxMessage);
        }

        await _repository.SaveChangesAsync(ct);

        return entitiesWithEvents.Count;
    }
}
