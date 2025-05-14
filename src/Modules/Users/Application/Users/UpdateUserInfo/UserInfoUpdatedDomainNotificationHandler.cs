using Mediator;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Users.Application.Users.UpdateUserInfo;

public class UserInfoUpdatedDomainNotificationHandler(IMediator mediator)
    : INotificationHandler<UserInfoUpdatedDomainNotification>
{
    private readonly IMediator _mediator = mediator;

    public async ValueTask Handle(UserInfoUpdatedDomainNotification notification, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new UserInforUpdatedIntegrationEvent(
            notification.Event.UserId,
            notification.Event.Username,
            notification.Event.Nickname,
            notification.Event.DateOfBirth,
            notification.Event.ProfilePicture,
            notification.Event.ProfileIsPublic
            ));
    }
}
