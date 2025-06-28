using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.FollowUser;

internal sealed class UserFollowedDomainEventNotificationHandler(IEventBus eventBus)
    : INotificationHandler<UserFollowedDomainEventNotification>
{
    private readonly IEventBus _eventBus = eventBus;

    public async ValueTask Handle(UserFollowedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _eventBus.Publish(new UserFollowedIntegrationEvent(
            notification.Event.FollowerId.Value,
            notification.Event.FollowingId.Value,
            notification.Event.FollowedAt
            ));
    }
}
