namespace SocialMediaBackend.Domain.Users.Follows;

public class FollowRequestRejectedEvent(UserId followerId, UserId followingId) 
: FollowEventBase(followerId, followingId);
