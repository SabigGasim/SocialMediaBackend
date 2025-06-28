using Mediator;

namespace SocialMediaBackend.BuildingBlocks.Domain;

public interface IEvent : INotification
{
    Guid Id { get; }
    DateTimeOffset OccurredOn { get; }
}
