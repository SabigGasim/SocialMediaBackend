using Mediator;
using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Users.Follows;

public enum FollowStatus
{
    Following, Pending
}

public class Follow : Entity<Unit>
{
    private Follow() { }

    private Follow(Guid followerId, Guid followingId, FollowStatus status)
    {
        FollowerId = followerId;
        FollowingId = followingId;
        FollowedAt = TimeProvider.System.GetUtcNow();
        Status = status;
    }

    public Guid FollowerId { get; }
    public Guid FollowingId { get; }

    public DateTimeOffset FollowedAt { get; }
    public FollowStatus Status { get; private set; }

    public User Follower { get; } = default!;
    public User Following { get; } = default!;

    internal static Follow CreateFollowRequest(Guid followerId, Guid followingId)
    {
        return new Follow(followerId, followingId, FollowStatus.Pending);
    }

    internal static Follow Create(Guid followerId, Guid followingId)
    {
        return new Follow(followerId, followingId, FollowStatus.Following);
    }

    internal bool AcceptFollowRequest()
    {
        Status = FollowStatus.Following;
        return true;
    }
}
