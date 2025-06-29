namespace SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

public record StreamEventBase : IStreamEvent
{
    public Guid Id { get; } = Guid.CreateVersion7();
    public DateTimeOffset OccuredOn { get; } = DateTimeOffset.UtcNow;
}
