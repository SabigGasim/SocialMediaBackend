namespace SocialMediaBackend.Modules.Payments.Contracts.Gateway;

public record CreateCheckoutSessionResponse(
    string SessionId,
    string CheckoutUrl,
    string ClientSecret // For using Stripe Elements instead of redirect
);