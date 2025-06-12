namespace SocialMediaBackend.BuildingBlocks.Domain;

public interface IStreamAggregate : IHasDomainEvents
{
    Guid Id { get; }
    IReadOnlyCollection<IStreamEvent> Events { get; }
}
