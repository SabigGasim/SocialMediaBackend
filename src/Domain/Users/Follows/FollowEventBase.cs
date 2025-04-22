using SocialMediaBackend.Domain.Common;

namespace SocialMediaBackend.Domain.Users.Follows;

public abstract class FollowEventBase(Guid followerId, Guid followingId) : DomainEventBase
{
    public Guid FollowerId { get; } = followerId;
    public Guid FollowingId { get; } = followingId;

    public void Deconstruct(out Guid followerId, out Guid followingId)
    {
        followerId = FollowerId;
        followingId = FollowingId;
    }
}
