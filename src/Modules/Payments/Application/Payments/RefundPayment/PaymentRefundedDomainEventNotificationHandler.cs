using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.RefundPayment;

internal sealed class PaymentRefundedDomainEventNotificationHandler(IEventBus eventBus)
    : INotificationHandler<PaymentRefundedDomainEventNotification>
{
    private readonly IEventBus _eventBus = eventBus;

    public async ValueTask Handle(PaymentRefundedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _eventBus.Publish(new PaymentRefnudedIntegrationEvent(
            notification.Event.PurchaseId.Value,
            notification.Event.PayerId.Value,
            notification.Event.ProductReference,
            notification.Event.RefundedAt)
            );
    }
}
