using Mediator;
using SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.CancelSubscription;

public class SubscriptionCancelledDomainEventNotificationHandler(IMediator mediator)
    : INotificationHandler<SubscriptionCancelledDomainEventNotification>
{
    private readonly IMediator _mediator = mediator;

    public async ValueTask Handle(SubscriptionCancelledDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new SubscriptionCancelledIntegrationEvent(
            notification.Event.PayerId.Value,
            notification.Event.SubscriptionId.Value,
            notification.Event.ProductReference),
            CancellationToken.None
            );
    }
}
