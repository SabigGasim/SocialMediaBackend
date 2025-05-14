using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Users.Domain.Users.Events;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateUserInfo;

public class UserInfoUpdatedDomainNotification(UserInfoUpdatedDomainEvent domainEvent, Guid id)
    : DomainNotificationBase<UserInfoUpdatedDomainEvent>(domainEvent, id);
