using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Users.Domain.Users.Follows;

public abstract class FollowEventBase(UserId followerId, UserId followingId) : DomainEventBase
{
    public UserId FollowerId { get; } = followerId;
    public UserId FollowingId { get; } = followingId;

    public void Deconstruct(out UserId followerId, out UserId followingId)
    {
        followerId = FollowerId;
        followingId = FollowingId;
    }
}
