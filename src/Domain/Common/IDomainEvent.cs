using Mediator;

namespace SocialMediaBackend.Domain.Common;


public interface IDomainEvent : INotification
{
    DateTimeOffset OccuredOn { get; }
}
