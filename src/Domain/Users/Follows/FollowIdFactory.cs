namespace SocialMediaBackend.Domain.Users.Follows;

public static class FollowIdFactory
{
    public static object[] Create(Guid followerId, Guid followingId)
    {
        return [followerId, followingId];
    }
}
