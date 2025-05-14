using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Users.Domain.Users.Events;

namespace SocialMediaBackend.Modules.Users.Application.Users.DeleteUser;

public class UserDeletedDomainEventNotification(UserDeletedDomainEvent domainEvent, Guid id)
    : DomainNotificationBase<UserDeletedDomainEvent>(domainEvent, id);
