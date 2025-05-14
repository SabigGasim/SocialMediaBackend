using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Users.Domain.Users.Events;

public class UserDeletedDomainEvent(UserId userId) : DomainEventBase
{
    public UserId UserId { get; } = userId;
}
