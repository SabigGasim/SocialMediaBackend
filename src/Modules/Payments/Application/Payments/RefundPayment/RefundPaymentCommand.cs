using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Domain.Purchase;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.RefundPayment;

public class RefundPaymentCommand(
    PurchaseId purchaseId,
    DateTimeOffset refundedAt,
    Guid id = default) : InternalCommandBase(id)
{
    public PurchaseId PurchaseId { get; } = purchaseId;
    public DateTimeOffset RefundedAt { get; } = refundedAt;
}
