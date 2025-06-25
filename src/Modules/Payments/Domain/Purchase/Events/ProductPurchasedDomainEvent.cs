using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Payers;

namespace SocialMediaBackend.Modules.Payments.Domain.Purchase.Events;

public class ProductPurchasedDomainEvent(
    PurchaseId purchaseId,
    PayerId payerId,
    string productReference,
    DateTimeOffset purchasedAt) : DomainEventBase
{
    public PurchaseId PurchaseId { get; } = purchaseId;
    public PayerId PayerId { get; } = payerId;
    public string ProductReference { get; } = productReference;
    public DateTimeOffset PurchasedAt { get; } = purchasedAt;
}
