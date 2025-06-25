using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Payers;

namespace SocialMediaBackend.Modules.Payments.Domain.Purchase.Events;

public class PaymentRefundedDomainEvent(
    PurchaseId purchaseId,
    PayerId payerId,
    DateTimeOffset refundedAt) : DomainEventBase
{
    public PurchaseId PurchaseId { get; } = purchaseId;
    public PayerId PayerId { get; } = payerId;
    public DateTimeOffset RefundedAt { get; } = refundedAt;
}
