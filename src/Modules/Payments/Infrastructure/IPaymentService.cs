using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.ValueObjects;
using Stripe;

namespace SocialMediaBackend.Modules.Payments.Infrastructure;

public interface IPaymentService
{
    Task<PaymentIntent> ConfirmPaymentIntentAsync(string paymentIntentId, string paymentMethodId);
    Task<PaymentIntent> CreatePaymentIntentAsync(MoneyValue moneyValue);
    Task<PaymentMethod> CreatePaymentMethodAsync();
    Task<Customer> CreateCustomerAsync(PayerId payerId);
}
