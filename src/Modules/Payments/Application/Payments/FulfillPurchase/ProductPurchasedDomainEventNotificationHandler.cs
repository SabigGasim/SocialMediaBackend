using Mediator;
using SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.FulfillPurchase;

public class ProductPurchasedDomainEventNotificationHandler(IMediator mediator)
    : INotificationHandler<ProductPurchasedDomainEventNotification>
{
    private readonly IMediator _mediator = mediator;

    public async ValueTask Handle(ProductPurchasedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _mediator.Publish(new ProductPurchasedIntegrationEvent(
            notification.Event.PurchaseId.Value,
            notification.Event.PayerId.Value,
            notification.Event.ProductReference,
            notification.Event.PurchasedAt),
            CancellationToken.None
            );
    }
}
