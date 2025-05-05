using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.AcceptFollowRequest;

public sealed class FollowRequestAcceptedEventHandler(UsersDbContext context)
    : FollowUserEventHandlerBase<FollowRequestAcceptedEvent>(context)
{
    protected override Task ApplyChanges(User follower, User following, CancellationToken ct = default)
    {
        follower.IncrementFollowingCount(1);
        following.IncrementFollowersCount(1);

        return Task.CompletedTask;
    }
}