using Marten;
using SocialMediaBackend.BuildingBlocks.Domain;

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

    public void StartStream<TAggregate>(TAggregate aggregate) where TAggregate : class, IStreamAggregate
    {
        _documentSession.Events.StartStream<TAggregate>(aggregate.Id, aggregate.StreamEvents);

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
}
