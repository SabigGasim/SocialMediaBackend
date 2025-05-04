namespace SocialMediaBackend.Modules.Users.Domain.Users.Follows;

public class FollowRequestRejectedEvent(UserId followerId, UserId followingId) 
: FollowEventBase(followerId, followingId);
