using Mediator;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Users.Application.Users.DeleteUser;

public class UserDeletedDomainEventNotificationHandler(IMediator mediator)
    : INotificationHandler<UserDeletedDomainEventNotification>
{
    private readonly IMediator _mediator = mediator;

    public async ValueTask Handle(UserDeletedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new UserDeletedIntegrationEvent(notification.Event.UserId.Value));
    }
}
