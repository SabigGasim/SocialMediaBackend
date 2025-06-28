using SocialMediaBackend.BuildingBlocks.Application;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows;

internal abstract class FollowUserEventHandlerBase<TFollowEventBase>(UsersDbContext context)
    : IDomainEventHandler<TFollowEventBase>
    where TFollowEventBase : FollowEventBase
{
    private readonly UsersDbContext _context = context;

    protected abstract Task ApplyChanges(User follower, User following, CancellationToken ct = default);

    public async ValueTask Handle(TFollowEventBase domainEvent, CancellationToken cancellationToken)
    {
        var (followerId, followingId) = domainEvent;

        var follower = await _context.Users.FindAsync([followerId], cancellationToken);
        var following = await _context.Users.FindAsync([followingId], cancellationToken);

        if (follower is null || following is null)
        {
            // TODO: Handle rollback
            return;
        }

        await ApplyChanges(follower, following, cancellationToken);
    }
}
