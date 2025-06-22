using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Payments.Contracts;
using SocialMediaBackend.Modules.Payments.Domain.Products;

namespace SocialMediaBackend.Modules.Payments.Application.Products.CreateProductPrice;

public class CreateProductPriceCommand(Guid productId, ProductPrice productPrice) : CommandBase<ProductPriceId>
{
    public Guid ProductId { get; } = productId;
    public ProductPrice ProductPrice { get; } = productPrice;
}
