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
    private Follow(AuthorId followerId, AuthorId followingId, DateTimeOffset followedAt, FollowStatus status)
    {
        FollowerId = followerId;
        FollowingId = followingId;
        FollowedAt = followedAt;
        Status = status;
    }

    public AuthorId FollowerId { get; } = default!;
    public AuthorId FollowingId { get; } = default!;

    public DateTimeOffset FollowedAt { get; private set; }
    public FollowStatus Status { get; private set; }

    public Author Follower { get; } = default!;
    public Author Following { get; } = default!;

    internal static Follow Create(AuthorId followerId, AuthorId followingId, DateTimeOffset followedAt, FollowStatus status)
    {
        return new Follow(followerId, followingId, followedAt, status);
    }
}
