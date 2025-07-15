using Autofac;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public sealed class EFUnitOfWork(
    DbContext context, 
    IDomainEventsDispatcher dispatcher,
    ILifetimeScope scope) : IUnitOfWork
{
    private readonly DbContext _context = context;
    private readonly IDomainEventsDispatcher _dispatcher = dispatcher;
    private readonly ILifetimeScope _scope = scope;

    public async Task<int> CommitAsync(CancellationToken ct = default)
    {
        while (TryGetDomainEvents(out var entities, out var domainEvents, out var notifications))
        {
            foreach (var entity in entities)
            {
                entity.ClearDomainEvents();
            }

            await _dispatcher.DispatchAsync(domainEvents, ct);

            foreach (var notification in notifications)
            {
                var outboxMessage = OutboxMessage.Create(notification);

                _context.Set<OutboxMessage>().Add(outboxMessage);
            }
        }

        return await _context.SaveChangesAsync(ct);
    }

    private bool TryGetDomainEvents(
        out IHasDomainEvents[] entities,
        out IDomainEvent[] domainEvents,
        out List<IDomainEventNotification> notifications)
    {
        entities = _context.ChangeTracker
            .Entries<IHasDomainEvents>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count > 0)
            .ToArray();

        if (entities.Length == 0) 
        {
            domainEvents = [];
            notifications = [];
            return false;
        }

        domainEvents = entities
            .SelectMany(e => e.DomainEvents)
            .ToArray();

        notifications = [];
        foreach (var domainEvent in domainEvents)
        {
            Type notificationType = typeof(IDomainEventNotification<>);
            Type NotificationGenericType = notificationType.MakeGenericType(domainEvent.GetType());
            
            var notification = _scope.ResolveOptional(NotificationGenericType, new List<NamedParameter>
            {
                new NamedParameter("id", domainEvent.Id),
                new NamedParameter("domainEvent", domainEvent)
            });

            if (notification is not null)
            {
                notifications.Add((IDomainEventNotification)notification);
            }
        }

        return true;
    }
}
