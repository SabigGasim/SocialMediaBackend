using Autofac;
using Autofac.Core;
using Mediator;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;

public sealed class EventSourcingUnitOfWork(
    IAggregateRepository repository,
    IAggregateTracker aggregateTracker,
    DbContext context,
    IDomainEventsDispatcher dispatcher,
    ILifetimeScope scope) : IUnitOfWork
{
    private readonly IAggregateRepository _repository = repository;
    private readonly IAggregateTracker _aggregateTracker = aggregateTracker;
    private readonly DbContext _context = context;
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

                domainEventNotifications.Add(notification!);
            }
        }

        foreach (var entity in entitiesWithEvents)
        {
            entity.ClearDomainEvents();
        }

        await _dispatcher.DispatchAsync(domainEvents, ct);

        await _repository.SaveChangesAsync(ct);

        if (_context.ChangeTracker.HasChanges())
        {
            await _context.SaveChangesAsync(ct);
        }

        foreach (var notification in domainEventNotifications)
        {
            var mediator = _scope.Resolve<IMediator>();
            await mediator.Publish(notification, CancellationToken.None);
        }

        return entitiesWithEvents.Count;
    }
}
