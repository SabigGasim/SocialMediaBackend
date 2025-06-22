using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Payments.Domain.Products;

namespace SocialMediaBackend.Modules.Payments.Application.Products.CreateProduct;

public class CreateProductCommand(string productReference, string name, string description, string owner)
    : CommandBase<ProductId>
{
    public string ProductReference { get; } = productReference;
    public string Name { get; } = name;
    public string Description { get; } = description;
    public string Owner { get; } = owner;
}
