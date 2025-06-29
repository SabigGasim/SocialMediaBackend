namespace SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

public interface IStreamAggregate : IHasDomainEvents
{
    Guid Id { get; }
    IReadOnlyCollection<IStreamEvent> UnCommittedEvents { get; }
    void ClearStreamEvents();
}
