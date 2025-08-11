using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;
using System.Linq.Expressions;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;

public interface IAggregateRepository
{
    Task<TAggregate> LoadAsync<TAggregate>(Guid aggregateId, CancellationToken ct = default)
        where TAggregate : class, IStreamAggregate;
    Task<TAggregate> LoadAsync<TAggregate>(Expression<Func<TAggregate, bool>> expression, CancellationToken ct = default)
        where TAggregate : class, IStreamAggregate;
    Task<TAggregate?> LoadOrDefaultAsync<TAggregate>(Guid aggregateId, CancellationToken ct = default)
        where TAggregate : class, IStreamAggregate;
    Task<TAggregate?> LoadOrDefaultAsync<TAggregate>(Expression<Func<TAggregate, bool>> expression, CancellationToken ct = default)
        where TAggregate : class, IStreamAggregate;
    Task<IEnumerable<TAggregate>> LoadManyAsync<TAggregate>(Expression<Func<TAggregate, bool>> expression, CancellationToken ct = default)
        where TAggregate : class, IStreamAggregate;
    Task<IEnumerable<TAggregate>> LoadManyAsync<TAggregate, TKey>(
        Expression<Func<TAggregate, bool>> expression,
        Expression<Func<TAggregate, TKey>> orderBy, 
        bool descending = false, 
        CancellationToken ct = default) 
        where TAggregate : class, IStreamAggregate;
    Task SaveChangesAsync(CancellationToken ct = default);
    void StartStream<TAggregate>(TAggregate aggregate) where TAggregate : class, IStreamAggregate;
    void Append(Guid streamId, IEnumerable<IStreamEvent> events);
    void AppendUnCommittedEvents<TAggregate>(TAggregate aggregate) where TAggregate : class, IStreamAggregate;
    void Store<TAggregate>(TAggregate aggregate) where TAggregate : class, IStreamAggregate;
}
