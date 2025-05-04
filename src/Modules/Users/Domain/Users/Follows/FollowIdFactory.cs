namespace SocialMediaBackend.Modules.Users.Domain.Users.Follows;

public static class FollowIdFactory
{
    public static object[] Create(UserId followerId, UserId followingId)
    {
        return [followerId, followingId];
    }
}
