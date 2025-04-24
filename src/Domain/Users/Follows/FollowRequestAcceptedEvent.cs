namespace SocialMediaBackend.Domain.Users.Follows;

public class FollowRequestAcceptedEvent(UserId followerId, UserId followingId) : FollowEventBase(followerId, followingId);
