using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Users.Follows;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.Follows.FollowUser;

public class UserFollowedEventHandler(ApplicationDbContext context) 
    : FollowUserEventHandlerBase<UserFollowedEvent>(context)
{
    protected override void ApplyChanges(User follower, User following)
    {
        follower.IncrementFollowingCount(1);
        following.IncrementFollowersCount(1);
    }
}
