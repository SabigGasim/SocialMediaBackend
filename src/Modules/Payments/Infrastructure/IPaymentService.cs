using SocialMediaBackend.Modules.Payments.Contracts;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using Stripe;

namespace SocialMediaBackend.Modules.Payments.Infrastructure;

public interface IPaymentService
{
    Task<PaymentIntent> ConfirmPaymentIntentAsync(string paymentIntentId, string paymentMethodId);
    Task<PaymentIntent> CreatePaymentIntentAsync(string customerId, MoneyValue moneyValue);
    Task<PaymentMethod> CreatePaymentMethodAsync();
    Task<Customer> CreateCustomerAsync(PayerId payerId);
    Task<string> CreateProductAsync(string productReference, string name, string description);
    Task ArchiveProductAsync(string gatewayProductId);
    Task<string> CreatePriceAsync(string productId, ProductPrice productPrice);
}
