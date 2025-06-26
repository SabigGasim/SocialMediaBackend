using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.Modules.Payments.Domain.Purchase;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.CancelPurchase;

public class CancelPurchaseCommand(
    PurchaseId purchaseId,
    Guid id = default) : InternalCommandBase(id)
{
    public PurchaseId PurchaseId { get; } = purchaseId;
}
