using Mediator;
using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;

namespace SocialMediaBackend.Modules.Chat.Domain.Follows;

public enum FollowStatus
{
    Following, Pending
}

public class Follow : Entity<Unit>
{
    private Follow() { }

    private Follow(ChatterId followerId, ChatterId followingId, FollowStatus status)
    {
        FollowerId = followerId;
        FollowingId = followingId;
        FollowedAt = TimeProvider.System.GetUtcNow();
        Status = status;
    }

    public ChatterId FollowerId { get; } = default!;
    public ChatterId FollowingId { get; } = default!;

    public DateTimeOffset FollowedAt { get; private set; }
    public FollowStatus Status { get; private set; }

    public Chatter Follower { get; } = default!;
    public Chatter Following { get; } = default!;

    internal static Follow CreateFollowRequest(ChatterId followerId, ChatterId followingId)
    {
        return new Follow(followerId, followingId, FollowStatus.Pending);
    }

    internal static Follow Create(ChatterId followerId, ChatterId followingId)
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
