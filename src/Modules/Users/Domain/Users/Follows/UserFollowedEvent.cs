namespace SocialMediaBackend.Modules.Users.Domain.Users.Follows;

public class UserFollowedEvent(UserId followerId, UserId followingId) : FollowEventBase(followerId, followingId);
