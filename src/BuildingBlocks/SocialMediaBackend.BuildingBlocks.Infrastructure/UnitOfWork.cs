using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.BuildingBlocks.Infrastructure;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Processing;

public class UnitOfWork(DbContext context, IDomainEventsDispatcher dispatcher) : IUnitOfWork
{
    private readonly DbContext _context = context;
    private readonly IDomainEventsDispatcher _dispatcher = dispatcher;

    public async Task<int> CommitAsync(CancellationToken ct = default)
    {
        var entitiesWithEvents = _context.ChangeTracker.Entries<IHasDomainEvents>()
           .Select(e => e.Entity)
           .Where(e => e.DomainEvents?.Count > 0)
           .ToList();

        var events = entitiesWithEvents
            .SelectMany(e => e.DomainEvents!)
            .ToList();

        foreach (var entity in entitiesWithEvents)
        {
            entity.ClearDomainEvents();
        }

        await _dispatcher.DispatchAsync(events, ct);

        return await _context.SaveChangesAsync(ct);
    }
}
