using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

internal sealed class UserCreatedDomainEventNotificationHandler(IEventBus eventBus)
    : INotificationHandler<UserCreatedDomainEventNotification>
{
    private readonly IEventBus _eventBus = eventBus;

    public async ValueTask Handle(UserCreatedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _eventBus.Publish(new UserCreatedIntegrationEvent(
            notification.Event.UserId,
            notification.Event.Username,
            notification.Event.Nickname,
            notification.Event.DateOfBirth,
            notification.Event.ProfilePicture,
            notification.Event.ProfileIsPublic,
            followersCount: 0,
            followingCount: 0));
    }
}
