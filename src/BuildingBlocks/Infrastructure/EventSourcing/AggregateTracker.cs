using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;

public class AggregateTracker : IAggregateTracker
{
    private readonly List<IStreamAggregate> _trackedAggregates = new();

    public void Track<TAggregate>(TAggregate aggregate) where TAggregate : IStreamAggregate
    {
        if (!_trackedAggregates.Any(x => x is TAggregate && x.Id == aggregate.Id))
        {
            _trackedAggregates.Add(aggregate);
        }
    }

    public void Track<TAggregate>(IEnumerable<TAggregate> aggregates) where TAggregate : IStreamAggregate
    {
        foreach (var aggregate in aggregates)
        {
            this.Track(aggregate);
        }
    }

    public void ClearTrackedAggregates()
    {
        _trackedAggregates.Clear();
    }

    public IReadOnlyCollection<IStreamAggregate> GetTrackedAggregates()
    {
        return _trackedAggregates.AsReadOnly();
    }
}
