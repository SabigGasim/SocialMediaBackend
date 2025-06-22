using SocialMediaBackend.Modules.Payments.Contracts.Gateway;

namespace SocialMediaBackend.Modules.Payments.Contracts.Proxy;

public interface IPaymentAntiCorruptionLayer
{
    Task<Guid> CreateProductAsync(
        string productReference,
        string name,
        string description,
        string owner,
        CancellationToken ct = default);
    Task<Guid> CreatePriceAsync(Guid productId, ProductPrice productPrice, CancellationToken ct = default);
    Task<CreateCheckoutSessionResponse> CreateOneTimePaymentCheckoutSessionAsync(
        Guid userId,
        string productReference,
        string successUrl,
        string cancelUrl
        );
    Task<CreateCheckoutSessionResponse> CreateSubscriptionCheckoutSessionAsync(
        Guid userId,
        string productReference,
        Guid priceId,
        string successUrl,
        string cancelUrl,
        CancellationToken ct = default);
}
