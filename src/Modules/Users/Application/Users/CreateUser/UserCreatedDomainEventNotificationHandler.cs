using Mediator;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Users.Application.Users.CreateUser;

public class UserCreatedDomainEventNotificationHandler(IMediator mediator)
    : INotificationHandler<UserCreatedDomainEventNotification>
{
    private readonly IMediator _mediator = mediator;

    public async ValueTask Handle(UserCreatedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new UserCreatedIntegrationEvent(
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
