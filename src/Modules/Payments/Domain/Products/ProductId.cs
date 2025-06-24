using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Domain.Products;

public record ProductId(Guid Value) : TypedIdValueBase<Guid>(Value)
{
    public static ProductId New() => new(Guid.CreateVersion7());
}
