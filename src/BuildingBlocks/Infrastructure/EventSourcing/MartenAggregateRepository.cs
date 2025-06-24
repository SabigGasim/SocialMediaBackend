using Marten;
using SocialMediaBackend.BuildingBlocks.Domain;
using System.Linq.Expressions;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;

public class MartenAggregateRepository(
    IDocumentSession documentSession,
    IAggregateTracker tracker) : IAggregateRepository
{
    private readonly IDocumentSession _documentSession = documentSession;
    private readonly IAggregateTracker _tracker = tracker;

    public async Task<TAggregate?> LoadAsync<TAggregate>(Guid aggregateId, CancellationToken ct = default) 
        where TAggregate : class, IStreamAggregate
    {
        var aggregate = _tracker
            .GetTrackedAggregates()
            .FirstOrDefault(x => x.Id == aggregateId) as TAggregate
                ?? await _documentSession.LoadAsync<TAggregate>(aggregateId, ct);

        if (aggregate is null)
        {
            return null;
        }

        _tracker.Track(aggregate);
        
        return aggregate;
    }

    public async Task<TAggregate?> LoadAsync<TAggregate>(Expression<Func<TAggregate, bool>> expression, CancellationToken ct) 
        where TAggregate : class, IStreamAggregate
    {
        var aggregate = await _documentSession.Query<TAggregate>()
            .Where(expression)
            .FirstOrDefaultAsync(ct);

        if (aggregate is null)
        {
            return null;
        }

        _tracker.Track(aggregate);

        return aggregate;
    }

    public void StartStream<TAggregate>(TAggregate aggregate) where TAggregate : class, IStreamAggregate
    {
        _documentSession.Events.StartStream<TAggregate>(aggregate.Id, aggregate.UnCommittedEvents);

        aggregate.ClearStreamEvents();
        
        _tracker.Track(aggregate);
    }

    public void Append(Guid streamId, IEnumerable<IStreamEvent> events)
    {
        _documentSession.Events.Append(streamId, events);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await _documentSession.SaveChangesAsync(ct);
        
        _tracker.ClearTrackedAggregates();
    }

    public async Task<IEnumerable<TAggregate>> LoadManyAsync<TAggregate>(Expression<Func<TAggregate, bool>> expression, CancellationToken ct)
        where TAggregate : class, IStreamAggregate
    {
        var aggregates = await _documentSession.Query<TAggregate>()
            .Where(expression)
            .ToListAsync(ct);

        if (aggregates is not { Count: > 0 })
        {
            return [];
        }

        _tracker.Track(aggregates);

        return aggregates;
    }

    public void AppendUnCommittedEvents<TAggregate>(TAggregate aggregate)
        where TAggregate : class, IStreamAggregate
    {
        if (aggregate.UnCommittedEvents is not { Count: > 0 })
        {
            return;
        }

        _documentSession.Events.Append(aggregate.Id, aggregate.UnCommittedEvents);
        
        aggregate.ClearStreamEvents();
    }
}
