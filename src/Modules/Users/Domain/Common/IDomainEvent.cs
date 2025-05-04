using Mediator;

namespace SocialMediaBackend.Modules.Users.Domain.Common;


public interface IDomainEvent : INotification
{
    DateTimeOffset OccuredOn { get; }
}
