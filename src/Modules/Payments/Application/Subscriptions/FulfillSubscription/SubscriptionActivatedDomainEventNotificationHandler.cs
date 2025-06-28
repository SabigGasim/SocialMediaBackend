using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.FulfillSubscription;

internal sealed class SubscriptionActivatedDomainEventNotificationHandler(IEventBus eventBus)
    : INotificationHandler<SubscriptionActivatedDomainEventNotification>
{
    private readonly IEventBus _eventBus = eventBus;

    public async ValueTask Handle(SubscriptionActivatedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _eventBus.Publish(new SubscriptionActivatedIntegrationEvent(
            notification.Event.PayerId.Value,
            notification.Event.SubscriptionId.Value,
            notification.Event.ProductReference,
            notification.Event.ActivatedAt,
            notification.Event.ExpiresAt)
            );
    }
}
