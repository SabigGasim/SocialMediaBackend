using Mediator;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;

public class UserFollowedDomainEventNotificationHandler(IMediator mediator)
    : INotificationHandler<UserFollowedDomainEventNotification>
{
    private readonly IMediator _mediator = mediator;

    public async ValueTask Handle(UserFollowedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new UserFollowedIntegrationEvent(
            notification.Event.FollowerId.Value,
            notification.Event.FollowingId.Value,
            notification.Event.FollowedAt
            ));
    }
}
