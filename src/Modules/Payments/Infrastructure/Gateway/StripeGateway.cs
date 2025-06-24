using SocialMediaBackend.Modules.Payments.Contracts.Gateway;
using Stripe.Checkout;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Gateway;

public class StripeGateway : IPaymentGateway
{
    public Task<CreateCheckoutSessionResponse> CreateOneTimePaymentCheckoutSessionAsync(Guid userId, string productReference, string successUrl, string cancelUrl)
    {
        throw new NotImplementedException();
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
