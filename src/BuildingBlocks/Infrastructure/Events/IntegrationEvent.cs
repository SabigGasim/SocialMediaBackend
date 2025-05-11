using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

public abstract class IntegrationEvent : IEvent
{
    protected IntegrationEvent()
    {
        Id = Guid.CreateVersion7();
        OccuredOn = TimeProvider.System.GetUtcNow();
    }

    public Guid Id { get; }
    public DateTimeOffset OccuredOn { get; }
}
