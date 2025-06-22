using SocialMediaBackend.Modules.Payments.Application.Products.CreateProduct;
using SocialMediaBackend.Modules.Payments.Application.Products.CreateProductPrice;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.CreateSubscriptionCheckoutSession;
using SocialMediaBackend.Modules.Payments.Contracts;
using SocialMediaBackend.Modules.Payments.Contracts.Gateway;
using SocialMediaBackend.Modules.Payments.Contracts.Proxy;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.Products;
using SocialMediaBackend.Modules.Payments.Infrastructure.Helpers;

namespace SocialMediaBackend.Modules.Payments.Application;

public class PaymentAntiCorruptionLayer : IPaymentAntiCorruptionLayer
{
    public Task<CreateCheckoutSessionResponse> CreateOneTimePaymentCheckoutSessionAsync(Guid userId, string productReference, string successUrl, string cancelUrl)
    {
        throw new NotImplementedException();
    }

    public async Task<CreateCheckoutSessionResponse> CreateSubscriptionCheckoutSessionAsync(
        Guid userId, 
        string productReference, 
        Guid priceId, 
        string successUrl, 
        string cancelUrl, 
        CancellationToken ct = default)
    {
        var result = await CommandExecutor.ExecuteAsync<CreateSubscriptionCheckoutSessionCommand, CreateCheckoutSessionResponse>(
            new CreateSubscriptionCheckoutSessionCommand(
                new PayerId(userId),
                productReference,
                new ProductPriceId(priceId),
                successUrl,
                cancelUrl),
            ct);

        return result.Payload;
    }

    public async Task<Guid> CreateProductAsync(
        string productReference, 
        string name, 
        string description, 
        string owner,
        CancellationToken ct = default)
    {
        var result = await CommandExecutor.ExecuteAsync<CreateProductCommand, ProductId>(
            new CreateProductCommand(
                productReference,
                name,
                description,
                owner),
            ct);

        return result.Payload.Value;
    }

    public async Task<Guid> CreatePriceAsync(Guid productId, ProductPrice productPrice, CancellationToken ct = default)
    {
        var result = await CommandExecutor.ExecuteAsync<CreateProductPriceCommand, ProductPriceId>(
            new CreateProductPriceCommand(productId, productPrice),
            ct);

        return result.Payload.Value;
    }
}
