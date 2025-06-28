using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.Users.IntegrationEvents;

namespace SocialMediaBackend.Modules.Users.Application.Users.DeleteUser;

internal sealed class UserDeletedDomainEventNotificationHandler(IEventBus eventBus)
    : INotificationHandler<UserDeletedDomainEventNotification>
{
    private readonly IEventBus _eventBus = eventBus;

    public async ValueTask Handle(UserDeletedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _eventBus.Publish(new UserDeletedIntegrationEvent(notification.Event.UserId.Value));
    }
}
