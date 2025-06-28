using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateUserInfo;

internal sealed class UserInfoUpdatedDomainNotificationHandler(IEventBus eventBus)
    : INotificationHandler<UserInfoUpdatedDomainNotification>
{
    private readonly IEventBus _eventBus = eventBus;

    public async ValueTask Handle(UserInfoUpdatedDomainNotification notification, CancellationToken cancellationToken)
    {
        await _eventBus.Publish(new UserInforUpdatedIntegrationEvent(
            notification.Event.UserId,
            notification.Event.Username,
            notification.Event.Nickname,
            notification.Event.DateOfBirth,
            notification.Event.ProfilePicture,
            notification.Event.ProfileIsPublic
            ));
    }
}
