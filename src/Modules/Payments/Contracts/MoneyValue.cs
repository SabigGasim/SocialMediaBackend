namespace SocialMediaBackend.Modules.Payments.Contracts;

public record MoneyValue
{
    private MoneyValue() {}
    public MoneyValue(int amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public int Amount { get; init; }
    public Currency Currency { get; init; }

    public string CurrencyCode => Currency.ToString();
}