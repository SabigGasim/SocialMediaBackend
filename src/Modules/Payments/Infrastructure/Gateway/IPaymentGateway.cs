using SocialMediaBackend.Modules.Payments.Contracts.Gateway;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Gateway;

public interface IPaymentGateway
{
    Task<CreateCheckoutSessionResponse> CreateOneTimePaymentCheckoutSessionAsync(
        Guid userId,
        string productReference,
        string successUrl,
        string cancelUrl
        );

    Task<CreateCheckoutSessionResponse> CreateSubscriptionCheckoutSessionAsync(
        string gatewayCustomerId, 
        string productReference, 
        string gatewayPriceId,
        string successUrl, 
        string cancelUrl, 
        Guid internalSubscriptionId, 
        CancellationToken ct = default);
}
