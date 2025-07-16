using SocialMediaBackend.BuildingBlocks.Application.Requests;
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
    Task<HandlerResponse<CreateCheckoutSessionResponse>> CreateOneTimePaymentCheckoutSessionAsync(
        Guid userId,
        string productReference,
        Guid priceId,
        string successUrl,
        string cancelUrl,
        CancellationToken ct = default);

    Task<HandlerResponse> CancelSubscriptionAtPeriodEnd(Guid subscriptionId, CancellationToken ct);
    Task<HandlerResponse<CreateCheckoutSessionResponse>> CreateSubscriptionCheckoutSessionAsync(
        Guid userId,
        string productReference,
        Guid priceId,
        string successUrl,
        string cancelUrl,
        CancellationToken ct = default);
}
