using SocialMediaBackend.Modules.Payments.Contracts.Gateway;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Gateway;

public interface IPaymentGateway
{
    Task<CreateCheckoutSessionResponse> CreateBillingPortalSessionForSubscriptionUpgradeAsync(string customerId, string subscriptionId, string priceId);
    Task<CreateCheckoutSessionResponse> CreateOneTimePaymentCheckoutSessionAsync(
        string gatewayCustomerId,
        string productReference,
        string gatewayPriceId,
        string successUrl,
        string cancelUrl,
        Guid internalPaymentId,
        CancellationToken ct = default);

    Task<CreateCheckoutSessionResponse> CreateSubscriptionCheckoutSessionAsync(
        string gatewayCustomerId, 
        string productReference, 
        string gatewayPriceId,
        string successUrl, 
        string cancelUrl, 
        Guid internalSubscriptionId, 
        CancellationToken ct = default);
}
