using Mediator;

namespace SocialMediaBackend.BuildingBlocks.Domain;


public interface IDomainEvent : INotification
{
    DateTimeOffset OccuredOn { get; }
}
