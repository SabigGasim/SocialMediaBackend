using SocialMediaBackend.BuildingBlocks.Infrastructure.Events;

namespace SocialMediaBackend.Modules.Users.IntegrationEvents;

public class UserDeletedIntegrationEvent(Guid userId) : IntegrationEvent()
{
    public Guid UserId { get; } = userId;
}
