namespace SocialMediaBackend.Modules.Payments.Contracts.Gateway;

public record CreateCheckoutSessionResponse(
    string SessionId,
    string CheckoutUrl,
    string ClientSecret
);