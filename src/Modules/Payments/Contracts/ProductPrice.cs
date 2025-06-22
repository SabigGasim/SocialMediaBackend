namespace SocialMediaBackend.Modules.Payments.Contracts;

public record ProductPrice
{
    public MoneyValue MoneyValue { get; init; } = default!;
    public PaymentInterval PaymentInterval { get; init; }

    private ProductPrice() { } 

    public ProductPrice(MoneyValue moneyValue, PaymentInterval paymentInterval)
    {
        MoneyValue = moneyValue;
        PaymentInterval = paymentInterval;
    }
}
