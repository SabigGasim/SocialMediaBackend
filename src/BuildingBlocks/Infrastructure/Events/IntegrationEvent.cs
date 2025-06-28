using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

public abstract class IntegrationEvent : IEvent
{
    protected IntegrationEvent()
    {
        Id = Guid.CreateVersion7();
        OccurredOn = TimeProvider.System.GetUtcNow();
    }

    public Guid Id { get; }
    public DateTimeOffset OccurredOn { get; }
}
