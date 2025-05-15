namespace SocialMediaBackend.Modules.Users.Domain.Users.Follows;

public class UserFollowedEvent(
    UserId followerId,
    UserId followingId,
    DateTimeOffset followedAt) : FollowEventBase(followerId, followingId)
{
    public DateTimeOffset FollowedAt { get; } = followedAt;
}
