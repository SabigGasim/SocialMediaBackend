using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.CancelSubscription;

internal sealed class SubscriptionCancelledDomainEventNotificationHandler(IEventBus eventBus)
    : INotificationHandler<SubscriptionCancelledDomainEventNotification>
{
    private readonly IEventBus _eventBus = eventBus;

    public async ValueTask Handle(SubscriptionCancelledDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _eventBus.Publish(new SubscriptionCancelledIntegrationEvent(
            notification.Event.PayerId.Value,
            notification.Event.SubscriptionId.Value,
            notification.Event.ProductReference,
            notification.Event.CanceledAt)
            );
    }
}
