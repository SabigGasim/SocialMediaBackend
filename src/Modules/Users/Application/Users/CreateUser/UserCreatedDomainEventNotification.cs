using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Users.Domain.Users.Events;

namespace SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

public class UserCreatedDomainEventNotification(UserCreatedDomainEvent domainEvent, Guid id) 
    : DomainNotificationBase<UserCreatedDomainEvent>(domainEvent, id);