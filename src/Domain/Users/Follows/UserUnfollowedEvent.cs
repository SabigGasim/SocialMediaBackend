namespace SocialMediaBackend.Domain.Users.Follows;

public class UserUnfollowedEvent(Guid followerId, Guid followingId) : FollowEventBase(followerId, followingId);