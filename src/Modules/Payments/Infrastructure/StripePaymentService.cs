using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.ValueObjects;
using Stripe;

namespace SocialMediaBackend.Modules.Payments.Infrastructure;

public class StripePaymentService : IPaymentService
{
    public async Task<PaymentIntent> CreatePaymentIntentAsync(MoneyValue moneyValue)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = moneyValue.Amount,
            Currency = moneyValue.CurrencyCode,
            PaymentMethodTypes = new List<string> { "card" },
        };

        var paymentIntentService = new PaymentIntentService();
        return await paymentIntentService.CreateAsync(options);
    }

    public async Task<PaymentMethod> CreatePaymentMethodAsync()
    {
        var paymentMethodOptions = new PaymentMethodCreateOptions
        {
            Type = "card",
            Card = new PaymentMethodCardOptions
            {
                Token = "tok_visa"
            }
        };

        var paymentMethodService = new PaymentMethodService();
        return await paymentMethodService.CreateAsync(paymentMethodOptions);
    }

    public async Task<PaymentIntent> ConfirmPaymentIntentAsync(string paymentIntentId, string paymentMethodId)
    {
        var options = new PaymentIntentConfirmOptions
        {
            PaymentMethod = paymentMethodId,
            ReturnUrl = "https://example.com/return",
        };

        var paymentIntentService = new PaymentIntentService();
        return await paymentIntentService.ConfirmAsync(paymentIntentId, options);
    }

    public async Task<Customer> CreateCustomerAsync(PayerId payerId)
    {
        var service = new CustomerService();
        return await service.CreateAsync(new CustomerCreateOptions
        {
            Metadata = new Dictionary<string, string>
            {
                { "app_user_id",  payerId.Value.ToString() }
            }
        });
    }
}
