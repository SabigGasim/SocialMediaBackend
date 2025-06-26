using Mediator;
using SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.RefundPayment;

public class PaymentRefundedDomainEventNotificationHandler(IMediator mediator)
    : INotificationHandler<PaymentRefundedDomainEventNotification>
{
    private readonly IMediator _mediator = mediator;

    public async ValueTask Handle(PaymentRefundedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new PaymentRefnudedIntegrationEvent(
            notification.Event.PurchaseId.Value,
            notification.Event.PayerId.Value,
            notification.Event.ProductReference,
            notification.Event.RefundedAt),
            CancellationToken.None
            );
    }
}
