using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;

public interface IAggregateTracker
{
    void Track<TAggregate>(TAggregate aggregate) where TAggregate : IStreamAggregate;
    void Track<TAggregate>(IEnumerable<TAggregate> aggregate) where TAggregate : IStreamAggregate;
    IReadOnlyCollection<IStreamAggregate> GetTrackedAggregates();
    void ClearTrackedAggregates();
}
