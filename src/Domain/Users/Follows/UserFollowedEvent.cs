namespace SocialMediaBackend.Domain.Users.Follows;

public class UserFollowedEvent(Guid followerId, Guid followingId) : FollowEventBase(followerId, followingId);
