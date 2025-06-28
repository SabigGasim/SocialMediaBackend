using Mediator;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.Payments.Contracts.IntegrationEvents;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.FulfillPurchase;

internal sealed class ProductPurchasedDomainEventNotificationHandler(IEventBus eventBus)
    : INotificationHandler<ProductPurchasedDomainEventNotification>
{
    private readonly IEventBus _eventBus = eventBus;

    public async ValueTask Handle(ProductPurchasedDomainEventNotification notification, CancellationToken cancellationToken)
    {
        await _eventBus.Publish(new ProductPurchasedIntegrationEvent(
            notification.Event.PurchaseId.Value,
            notification.Event.PayerId.Value,
            notification.Event.ProductReference,
            notification.Event.PurchasedAt)
            );
    }
}
