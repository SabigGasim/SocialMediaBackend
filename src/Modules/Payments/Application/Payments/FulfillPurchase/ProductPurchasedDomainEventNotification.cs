using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Purchase.Events;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.FulfillPurchase;

public class ProductPurchasedDomainEventNotification(ProductPurchasedDomainEvent domainEvent, Guid id)
    : DomainNotificationBase<ProductPurchasedDomainEvent>(domainEvent, id);
