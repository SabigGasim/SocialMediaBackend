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

    public AuthorId FollowerId { get; } = default!;
    public AuthorId FollowingId { get; } = default!;

    public DateTimeOffset FollowedAt { get; private set; }
    public FollowStatus Status { get; private set; }

    public Author Follower { get; } = default!;
    public Author Following { get; } = default!;
}
