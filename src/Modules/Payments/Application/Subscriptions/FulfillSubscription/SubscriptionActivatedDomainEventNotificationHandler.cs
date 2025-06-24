using Mediator;
using SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.FulfillSubscription;

public class SubscriptionActivatedDomainEventNotificationHandler(IMediator mediator)
    : INotificationHandler<SubscriptionActivatedDomainEventNotification>
{
    private readonly IMediator _mediator = mediator;

    public async ValueTask Handle(SubscriptionActivatedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new SubscriptionActivatedIntegrationEvent(
            notification.Event.PayerId.Value,
            notification.Event.SubscriptionId.Value,
            notification.Event.ProductReference,
            notification.Event.ActivatedAt,
            notification.Event.ExpiresAt),
            CancellationToken.None
            );
    }
}
