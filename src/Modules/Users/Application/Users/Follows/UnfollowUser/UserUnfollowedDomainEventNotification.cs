using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.UnfollowUser;

public class UserUnfollowedDomainEventNotification(UserUnfollowedEvent domainEvent, Guid id)
    : DomainNotificationBase<UserUnfollowedEvent>(domainEvent, id);
