namespace SocialMediaBackend.BuildingBlocks.Domain;

public abstract class DomainEventBase : IDomainEvent
{
    protected DomainEventBase()
    {
        OccuredOn = TimeProvider.System.GetUtcNow();
    }

    public DateTimeOffset OccuredOn { get; }
}
