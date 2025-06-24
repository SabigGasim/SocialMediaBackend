using SocialMediaBackend.Modules.Payments.Contracts;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using Stripe;

namespace SocialMediaBackend.Modules.Payments.Infrastructure;

public class StripePaymentService : IPaymentService
{
    public async Task<PaymentIntent> CreatePaymentIntentAsync(string customerId, MoneyValue moneyValue)
    {
        var options = new PaymentIntentCreateOptions
        {
            Amount = moneyValue.Amount,
            Currency = moneyValue.CurrencyCode,
            PaymentMethodTypes = ["card"],
            Customer = customerId,
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
                { "user_id",  payerId.Value.ToString() }
            }
        });
    }

    public async Task<string> CreateProductAsync(
        string productReference,
        string name,
        string description)
    {
        var product = await new ProductService().CreateAsync(new ProductCreateOptions
        {
            Name = name,
            Description = description,
            Metadata = new Dictionary<string, string>
            {
                { "product_reference", productReference }
            },
        });

        return product.Id;
    }

    public async Task ArchiveProductAsync(string stripeProductId)
    {
        await new ProductService().UpdateAsync(stripeProductId, new ProductUpdateOptions
        {
            Active = false
        });
    }

    public async Task<string> CreatePriceAsync(string productId, ProductPrice productPrice)
    {
        var price = await new PriceService().CreateAsync(new PriceCreateOptions
        {
            Product = productId,
            UnitAmount = productPrice.MoneyValue.Amount,
            Currency = productPrice.MoneyValue.CurrencyCode,
            Recurring = productPrice.PaymentInterval switch
            {
                PaymentInterval.OneTime => null,
                PaymentInterval.Weekly => new() { Interval = "week" },
                PaymentInterval.Monthly => new() { Interval = "month" },
                PaymentInterval.HalfYear => new() { Interval = "month", IntervalCount = 6 },
                PaymentInterval.Yearly => new() { Interval = "year" },
                _ => throw new ArgumentOutOfRangeException(nameof(productPrice.PaymentInterval), productPrice.PaymentInterval, null)
            }
        });

        return price.Id;
    }

    public async Task CancelSubscriptionAsync(string subscriptionId)
    {
        await new SubscriptionService().CancelAsync(subscriptionId);
    }

    public async Task<IEnumerable<string>> GetCustomerPaymentMethodIdsAsync(string GatewayCustomerId)
    {
        var paymentMethods = await new CustomerPaymentMethodService().ListAsync(GatewayCustomerId);

        return paymentMethods.Select(x => x.Id);
    }

    public async Task DeletePaymentMethodAsync(string paymentMethodId)
    {
        await new PaymentMethodService().DetachAsync(paymentMethodId);
    }
}
