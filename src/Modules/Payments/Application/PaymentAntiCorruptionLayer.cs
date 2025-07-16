using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.Modules.Payments.Application.Payments.CreatePaymentCheckoutSession;
using SocialMediaBackend.Modules.Payments.Application.Products.CreateProduct;
using SocialMediaBackend.Modules.Payments.Application.Products.CreateProductPrice;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.CancelSubscriptionAtPeriodEnd;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.CreateSubscriptionCheckoutSession;
using SocialMediaBackend.Modules.Payments.Application.Subscriptions.ReactivateSubscription;
using SocialMediaBackend.Modules.Payments.Contracts;
using SocialMediaBackend.Modules.Payments.Contracts.Gateway;
using SocialMediaBackend.Modules.Payments.Contracts.Proxy;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.Products;
using SocialMediaBackend.Modules.Payments.Infrastructure.Helpers;

namespace SocialMediaBackend.Modules.Payments.Application;

public class PaymentAntiCorruptionLayer : IPaymentAntiCorruptionLayer
{
    public async Task<HandlerResponse<CreateCheckoutSessionResponse>> CreateOneTimePaymentCheckoutSessionAsync(
        Guid userId, 
        string productReference,
        Guid priceId,
        string successUrl, 
        string cancelUrl,
        CancellationToken ct = default)
    {
        return await CommandExecutor.ExecuteAsync<CreatePaymentCheckoutSessionCommand, CreateCheckoutSessionResponse>(
            new CreatePaymentCheckoutSessionCommand(
                new PayerId(userId),
                productReference,
                new ProductPriceId(priceId),
                successUrl,
                cancelUrl),
            ct);
    }

    public async Task<HandlerResponse<CreateCheckoutSessionResponse>> CreateSubscriptionCheckoutSessionAsync(
        Guid userId, 
        string productReference, 
        Guid priceId, 
        string successUrl, 
        string cancelUrl, 
        CancellationToken ct = default)
    {
        return await CommandExecutor.ExecuteAsync<CreateSubscriptionCheckoutSessionCommand, CreateCheckoutSessionResponse>(
            new CreateSubscriptionCheckoutSessionCommand(
                new PayerId(userId),
                productReference,
                new ProductPriceId(priceId),
                successUrl,
                cancelUrl),
            ct);
    }

    public async Task<HandlerResponse> CancelSubscriptionAtPeriodEnd(Guid subscriptionId, CancellationToken ct)
    {
        return await CommandExecutor.ExecuteAsync(
            new CancelSubscriptionAtPeriodEndCommand(subscriptionId),
            ct);
    }

    public async Task<HandlerResponse> ReactivateSubscription(Guid subscriptionId, CancellationToken ct)
    {
        return await CommandExecutor.ExecuteAsync(
            new ReactivateSubscriptionCommand(subscriptionId),
            ct);
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
