namespace SocialMediaBackend.BuildingBlocks.Domain;

public interface IHasDomainEvents
{
    IReadOnlyCollection<IDomainEvent>? DomainEvents { get; }
    void ClearDomainEvents();
}
