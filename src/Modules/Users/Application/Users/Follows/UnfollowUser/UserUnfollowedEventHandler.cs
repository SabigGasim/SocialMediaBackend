using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.UnfollowUser;

public sealed class UserUnfollowedEventHandler(UsersDbContext context)
    : FollowUserEventHandlerBase<UserUnfollowedEvent>(context)
{
    private readonly UsersDbContext _context = context;

    protected override async Task ApplyChanges(User follower, User following, CancellationToken ct = default)
    {
        var followId = FollowIdFactory.Create(follower.Id, following.Id);
        var follow = await _context.Follows.FindAsync(followId, ct);

        _context.Follows.Remove(follow!);

        follower.IncrementFollowingCount(-1);
        following.IncrementFollowersCount(-1);
    }
}
