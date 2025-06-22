using SocialMediaBackend.Modules.Payments.Contracts;

namespace SocialMediaBackend.Modules.Payments.Domain.Products;

[method: System.Text.Json.Serialization.JsonConstructor]
[method: Newtonsoft.Json.JsonConstructor]
public record ProductPlan(ProductPriceId Id, string GatewayPriceId, ProductPrice Price);