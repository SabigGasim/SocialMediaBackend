using Mediator;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.UnfollowUser;

public class UserUnfollowedDomainEventNotificationHandler(IMediator mediator)
    : INotificationHandler<UserUnfollowedDomainEventNotification>
{
    private readonly IMediator _mediator = mediator;

    public async ValueTask Handle(UserUnfollowedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new UserUnfollowedIntegrationEvent(
            notification.Event.FollowerId.Value,
            notification.Event.FollowingId.Value));
    }
}
