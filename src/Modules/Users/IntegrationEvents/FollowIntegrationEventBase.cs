using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.Users.IntegrationEvents;

public abstract class FollowIntegrationEventBase(
    Guid followerId, 
    Guid userId) : IntegrationEvent
{
    public Guid FollowerId { get; } = followerId;
    public Guid UserId { get; } = userId;
}
