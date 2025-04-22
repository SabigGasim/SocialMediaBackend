using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Users.Follows;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.Follows.AcceptFollowRequest;

public sealed class FollowRequestAcceptedEventHandler(ApplicationDbContext context)
    : FollowUserEventHandlerBase<FollowRequestAcceptedEvent>(context)
{
    protected override Task ApplyChanges(User follower, User following, CancellationToken ct = default)
    {
        follower.IncrementFollowingCount(1);
        following.IncrementFollowersCount(1);

        return Task.CompletedTask;
    }
}