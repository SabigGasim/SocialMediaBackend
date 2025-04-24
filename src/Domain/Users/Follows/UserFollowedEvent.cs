namespace SocialMediaBackend.Domain.Users.Follows;

public class UserFollowedEvent(UserId followerId, UserId followingId) : FollowEventBase(followerId, followingId);
