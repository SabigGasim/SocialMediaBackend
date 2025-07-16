using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.AppSubscriptions.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.Subscriptions.RenewSubscription;

internal sealed class SubscriptionRenewedNotificationHandler(IEventBus eventBus)
    : INotificationHandler<SubscriptionRenewedNotification>
{
    private readonly IEventBus _eventBus = eventBus;

    public async ValueTask Handle(SubscriptionRenewedNotification notification, CancellationToken cancellationToken)
    {
        await _eventBus.Publish(new AppSubscriptionRenewedIntegrationEvent(
            notification.Event.SubscriptionId.Value,
            notification.Event.SubscriberId.Value,
            notification.Event.ActivatedAt,
            notification.Event.ExpiresAt));
    }
}
