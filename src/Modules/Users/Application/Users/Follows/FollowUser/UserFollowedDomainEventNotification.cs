using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;
public class UserFollowedDomainEventNotification(UserFollowedEvent domainEvent, Guid id) 
    : DomainNotificationBase<UserFollowedEvent>(domainEvent, id);
