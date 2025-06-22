namespace SocialMediaBackend.BuildingBlocks.Domain;

public interface IStreamEvent
{
    Guid Id { get; }
    DateTimeOffset OccuredOn { get; }
}