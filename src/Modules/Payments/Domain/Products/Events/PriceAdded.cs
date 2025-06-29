using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;
using SocialMediaBackend.Modules.Payments.Contracts;

namespace SocialMediaBackend.Modules.Payments.Domain.Products.Events;

public record PriceAdded(ProductPriceId PriceId, string GatewayPriceId, ProductPrice Price) : StreamEventBase;
