using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Payments.Contracts.Gateway;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.Products;

namespace SocialMediaBackend.Modules.Payments.Application.Payments.CreatePaymentCheckoutSession;

public class CreatePaymentCheckoutSessionCommand(
    PayerId payerId,
    string productReference,
    ProductPriceId priceId,
    string successUrl,
    string cancelUrl) : CommandBase<CreateCheckoutSessionResponse>
{
    public PayerId PayerId { get; } = payerId;
    public string ProductReference { get; } = productReference;
    public ProductPriceId PriceId { get; } = priceId;
    public string SuccessUrl { get; } = successUrl;
    public string CancelUrl { get; } = cancelUrl;
}