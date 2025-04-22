using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Users.Follows;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.Follows.UnfollowUser;

public sealed class UserUnfollowedEventHandler(ApplicationDbContext context)
    : FollowUserEventHandlerBase<UserUnfollowedEvent>(context)
{
    private readonly ApplicationDbContext _context = context;

    protected override async Task ApplyChanges(User follower, User following, CancellationToken ct = default)
    {
        var followId = FollowIdFactory.Create(follower.Id, following.Id);
        var follow = await _context.Follows.FindAsync(followId, ct);

        _context.Follows.Remove(follow!);

        follower.IncrementFollowingCount(-1);
        following.IncrementFollowersCount(-1);
    }
}
