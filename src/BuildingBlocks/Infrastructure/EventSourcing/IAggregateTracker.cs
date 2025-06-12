using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;

public interface IAggregateTracker
{
    void Track<TAggregate>(TAggregate aggregate) where TAggregate : IStreamAggregate;
    IReadOnlyCollection<IStreamAggregate> GetTrackedAggregates();
    void ClearTrackedAggregates();
}
