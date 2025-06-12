using Autofac;
using Autofac.Core;
using Mediator;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Domain;

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
        var entitiesWithEvents = _context.ChangeTracker
            .Entries<IHasDomainEvents>()
           .Select(e => e.Entity)
            .Where(e => e.DomainEvents is { Count: > 0 })
            .ToArray();

        var domainEvents = entitiesWithEvents
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

        var result = await _context.SaveChangesAsync(ct);

        foreach (var notification in domainEventNotifications)
        {
            var mediator = _scope.Resolve<IMediator>();
            await mediator.Publish(notification, CancellationToken.None);
        }

        return result;
    }
}
