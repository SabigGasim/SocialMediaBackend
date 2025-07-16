using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.AppSubscriptions.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.ActivateSubscription;

internal sealed class SubscriptionActivatedNotificationHandler(IEventBus eventBus)
    : INotificationHandler<SubscriptionActivatedNotification>
{
    private readonly IEventBus _eventBus = eventBus;

    public async ValueTask Handle(SubscriptionActivatedNotification notification, CancellationToken cancellationToken)
    {
        await _eventBus.Publish(new AppSubscriptionActivatedIntegrationEvent(
            notification.Event.SubscriptionId.Value,
            notification.Event.SubscriberId.Value,
            notification.Event.SubscriptionTier,
            notification.Event.ActivatedAt,
            notification.Event.ExpiresAt));
    }
}
