using SocialMediaBackend.Modules.Payments.Contracts.Gateway;
using Stripe.Checkout;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Gateway;

public class StripeGateway : IPaymentGateway
{
    public async Task<CreateCheckoutSessionResponse> CreateOneTimePaymentCheckoutSessionAsync(
        string gatewayCustomerId, 
        string productReference,
        string gatewayPriceId, 
        string successUrl,
        string cancelUrl, 
        Guid internalPaymentId, 
        CancellationToken ct = default)
    {
        var options = new SessionCreateOptions
        {
            Customer = gatewayCustomerId,
            Mode = "payment",
            PaymentMethodTypes = ["card"], //Only for testing. Don't use this in production.
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl,
            LineItems =
            [
                new SessionLineItemOptions
                {
                    Price = gatewayPriceId,
                    Quantity = 1
                }
            ],
            PaymentIntentData = new SessionPaymentIntentDataOptions
            {
                Metadata = new Dictionary<string, string>
                {
                    { "internal_payment_id", internalPaymentId.ToString() },
                    { "product_reference", productReference }
                }
            },
            Metadata = new Dictionary<string, string>
            {
                { "mode", "payment" },
                { "internal_payment_id", internalPaymentId.ToString() },
                { "product_reference", productReference }
            },
            ExpiresAt = DateTime.UtcNow.AddMinutes(30)
        };

        var session = await new SessionService().CreateAsync(options, cancellationToken: ct);

        return new CreateCheckoutSessionResponse(
            session.Id,
            session.Url,
            session.ClientSecret
        );
    }

    public async Task<CreateCheckoutSessionResponse> CreateSubscriptionCheckoutSessionAsync(
        string gatewayCustomerId,
        string productReference,
        string gatewayPriceId,
        string successUrl,
        string cancelUrl,
        Guid internalSubscriptionId,
        CancellationToken ct = default)
    {
        var options = new SessionCreateOptions
        {
            Customer = gatewayCustomerId,
            Mode = "subscription",
            PaymentMethodTypes = ["card"], //Only for testing. Don't use this in production.
            SuccessUrl = successUrl,
            CancelUrl = cancelUrl,
            LineItems =
            [
                new SessionLineItemOptions
                {
                    Price = gatewayPriceId,
                    Quantity = 1
                }
            ],
            SubscriptionData = new SessionSubscriptionDataOptions
            {
                Metadata = new Dictionary<string, string>
                {
                    { "internal_subscription_id", internalSubscriptionId.ToString() },
                    { "product_reference", productReference },
                }
            },
            Metadata = new Dictionary<string, string> 
            {
                { "mode", "subscription" },
                { "internal_subscription_id", internalSubscriptionId.ToString() },
                { "product_reference", productReference }
            },
            ExpiresAt = DateTime.UtcNow.AddMinutes(30)
        };

        var session = await new SessionService().CreateAsync(options, cancellationToken: ct);

        return new CreateCheckoutSessionResponse(
            session.Id,
            session.Url,
            session.ClientSecret
        );
    }
}
