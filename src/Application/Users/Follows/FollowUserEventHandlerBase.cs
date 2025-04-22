using SocialMediaBackend.Application.Abstractions;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Users.Follows;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application.Users.Follows;

public abstract class FollowUserEventHandlerBase<TFollowEventBase>(ApplicationDbContext context)
    : IDomainEventHandler<TFollowEventBase>
    where TFollowEventBase : FollowEventBase
{
    private readonly ApplicationDbContext _context = context;

    protected abstract void ApplyChanges(User follower, User following);

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

        ApplyChanges(follower, following);
    }
}
