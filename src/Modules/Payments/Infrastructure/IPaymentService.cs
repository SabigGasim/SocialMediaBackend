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
    Task CancelSubscriptionAsync(string subscriptionId);
    Task<IEnumerable<string>> GetCustomerPaymentMethodIdsAsync(string GatewayCustomerId);
    Task DeletePaymentMethodAsync(string paymentMethodId);
    Task<bool> CancelSubscriptionAtPeriodEndAsync(string subscriptionId);
    Task<bool> ReactivateSubscriptionAsync(string subscriptionId);
    Task<Subscription> UpgradeSubscriptionPlanAsync(string subscriptionId, string newPriceId);
}
