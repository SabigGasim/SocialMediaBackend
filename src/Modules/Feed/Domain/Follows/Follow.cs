using Mediator;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Feed.Domain.Authors;

namespace SocialMediaBackend.Modules.Feed.Domain.Follows;

public enum FollowStatus
{
    Following, Pending
}

public class Follow : Entity<Unit>
{
    private Follow() { }

    private Follow(AuthorId followerId, AuthorId followingId, FollowStatus status)
    {
        FollowerId = followerId;
        FollowingId = followingId;
        FollowedAt = TimeProvider.System.GetUtcNow();
        Status = status;
    }

    public AuthorId FollowerId { get; } = default!;
    public AuthorId FollowingId { get; } = default!;

    public DateTimeOffset FollowedAt { get; private set; }
    public FollowStatus Status { get; private set; }

    public Author Follower { get; } = default!;
    public Author Following { get; } = default!;

    internal static Follow CreateFollowRequest(AuthorId followerId, AuthorId followingId)
    {
        return new Follow(followerId, followingId, FollowStatus.Pending);
    }

    internal static Follow Create(AuthorId followerId, AuthorId followingId)
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
