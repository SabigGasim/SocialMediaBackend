using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Products;

public record ProductPriceId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static ProductPriceId New() => new(Guid.NewGuid());
}
