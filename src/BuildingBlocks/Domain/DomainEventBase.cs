namespace SocialMediaBackend.BuildingBlocks.Domain;

public abstract class DomainEventBase : IDomainEvent
{
    protected DomainEventBase()
    {
        Id = Guid.NewGuid();
        OccuredOn = TimeProvider.System.GetUtcNow();
    }

    public Guid Id { get; }
    public DateTimeOffset OccuredOn { get; }
}
