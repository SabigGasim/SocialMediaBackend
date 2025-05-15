namespace SocialMediaBackend.Modules.Users.IntegrationEvents;
public class UserFollowedIntegrationEvent(
    Guid followerId,
    Guid userId,
    DateTimeOffset followedAt) : FollowIntegrationEventBase(followerId, userId)
{
    public DateTimeOffset FollowedAt { get; } = followedAt;
}
