namespace SocialMediaBackend.Modules.Payments.Contracts.Gateway;

public record CreateCheckoutSessionResponse(
    string SessionId,
    string CheckoutUrl,
    string ClientSecret,
    bool IsCompleted = false
)
{
    public static CreateCheckoutSessionResponse CreateCompleted() => new CreateCheckoutSessionResponse(
        SessionId: string.Empty,
        CheckoutUrl: string.Empty,
        ClientSecret: string.Empty,
        IsCompleted: true
    );
}