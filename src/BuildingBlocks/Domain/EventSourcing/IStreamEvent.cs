namespace SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;

public interface IStreamEvent
{
    Guid Id { get; }
    DateTimeOffset OccuredOn { get; }
}