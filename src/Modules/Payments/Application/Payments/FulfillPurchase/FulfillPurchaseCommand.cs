using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Domain.Purchase;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.FulfillPurchase;

public class FulfillPurchaseCommand(
    PurchaseId purchaseId,
    DateTimeOffset purchasedAt,
    Guid id = default) : InternalCommandBase(id)
{
    public PurchaseId PurchaseId { get; } = purchaseId;
    public DateTimeOffset PurchasedAt { get; } = purchasedAt;
}
