using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Contracts;

namespace SocialMediaBackend.Modules.Payments.Domain.Products.Events;

public record PriceChanged(ProductPriceId PriceId, ProductPrice NewPrice) : StreamEventBase;
