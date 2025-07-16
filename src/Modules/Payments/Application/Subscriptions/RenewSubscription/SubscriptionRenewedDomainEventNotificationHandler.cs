using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.RenewSubscription;

internal sealed class SubscriptionRenewedNotificationHandler(IEventBus eventBus)
    : INotificationHandler<SubscriptionRenewedNotification>
{
    private readonly IEventBus _eventBus = eventBus;

    public async ValueTask Handle(SubscriptionRenewedNotification notification, CancellationToken cancellationToken)
    {
        await _eventBus.Publish(new SubscriptionRenewedIntegrationEvent(
            notification.Event.PayerId.Value,
            notification.Event.SubscriptionId.Value,
            notification.Event.ProductReference,
            notification.Event.RenewedAt,
            notification.Event.ExpiresAt)
            );
    }
}
