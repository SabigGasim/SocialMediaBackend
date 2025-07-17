using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.AppSubscriptions.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.CancelSubscription;

internal sealed class AppSubscriptionCanceledNotificationHandler(IEventBus eventBus)
    : INotificationHandler<AppSubscriptionCanceledNotification>
{
    private readonly IEventBus _eventBus = eventBus;

    public async ValueTask Handle(AppSubscriptionCanceledNotification notification, CancellationToken cancellationToken)
    {
        await _eventBus.Publish(new AppSubscriptionCanceledIntegrationEvent(
            notification.Event.SubscriptionId.Value,
            notification.Event.SubscriberId.Value,
            notification.Event.CanceledAt
        ));
    }
}
