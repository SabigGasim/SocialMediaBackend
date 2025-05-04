using Mediator;
using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Users.Domain.Users.Follows;

public enum FollowStatus
{
    Following, Pending
}

public class Follow : Entity<Unit>
{
    private Follow() { }

    private Follow(UserId followerId, UserId followingId, FollowStatus status)
    {
        FollowerId = followerId;
        FollowingId = followingId;
        FollowedAt = TimeProvider.System.GetUtcNow();
        Status = status;
    }

    public UserId FollowerId { get; } = default!;
    public UserId FollowingId { get; } = default!;

    public DateTimeOffset FollowedAt { get; }
    public FollowStatus Status { get; private set; }

    public User Follower { get; } = default!;
    public User Following { get; } = default!;

    internal static Follow CreateFollowRequest(UserId followerId, UserId followingId)
    {
        return new Follow(followerId, followingId, FollowStatus.Pending);
    }

    internal static Follow Create(UserId followerId, UserId followingId)
    {
        return new Follow(followerId, followingId, FollowStatus.Following);
    }

    internal bool AcceptFollowRequest()
    {
        if (Status == FollowStatus.Pending)
        {
            Status = FollowStatus.Following;
            return true;
        }

        return false;
    }
}
