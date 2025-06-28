using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Users.Application.Users.Follows.UnfollowUser;

internal sealed class UserUnfollowedDomainEventNotificationHandler(IEventBus eventBus)
    : INotificationHandler<UserUnfollowedDomainEventNotification>
{
    private readonly IEventBus _eventBus = eventBus;

    public async ValueTask Handle(UserUnfollowedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _eventBus.Publish(new UserUnfollowedIntegrationEvent(
            notification.Event.FollowerId.Value,
            notification.Event.FollowingId.Value));
    }
}
