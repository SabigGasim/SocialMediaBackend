using SocialMediaBackend.BuildingBlocks.Domain;

namespace SocialMediaBackend.Modules.Payments.Contracts;

public record MoneyValue : ValueObject
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

    protected override IEnumerable<object> GetComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}