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

    public ChatterId FollowerId { get; } = default!;
    public ChatterId FollowingId { get; } = default!;

    public DateTimeOffset FollowedAt { get; private set; }
    public FollowStatus Status { get; private set; }

    public Chatter Follower { get; } = default!;
    public Chatter Following { get; } = default!;
}
