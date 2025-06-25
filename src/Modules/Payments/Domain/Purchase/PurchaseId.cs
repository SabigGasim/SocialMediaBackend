using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Purchase;

public record PurchaseId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static PurchaseId New() => new(Guid.CreateVersion7());
}
