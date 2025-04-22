namespace SocialMediaBackend.Domain.Users.Follows;

public class FollowRequestRejectedEvent(Guid followerId, Guid followingId) 
: FollowEventBase(followerId, followingId);
