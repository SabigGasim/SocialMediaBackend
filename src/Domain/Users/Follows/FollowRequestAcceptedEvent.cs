namespace SocialMediaBackend.Domain.Users.Follows;

public class FollowRequestAcceptedEvent(Guid followerId, Guid followingId) : FollowEventBase(followerId, followingId);
