
namespace SocialMediaBackend.Modules.Users.IntegrationEvents;

public class UserUnfollowedIntegrationEvent(Guid followerId, Guid userId) 
    : FollowIntegrationEventBase(followerId, userId);
