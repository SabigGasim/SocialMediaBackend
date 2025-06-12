using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;

public interface IAggregateRepository
{
    Task<TAggregate?> LoadAsync<TAggregate>(Guid aggregateId, CancellationToken ct = default)
        where TAggregate : class, IStreamAggregate;
    Task SaveChangesAsync(CancellationToken ct = default);
    void StartStream<TAggregate>(TAggregate aggregate) where TAggregate : class, IStreamAggregate;
}
