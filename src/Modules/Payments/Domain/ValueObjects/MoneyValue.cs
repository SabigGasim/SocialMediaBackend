using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Domain.Common;

namespace SocialMediaBackend.Modules.Payments.Domain.ValueObjects;

public record MoneyValue : ValueObject
{
    public int Amount { get; private set; }
    public Currency Currency { get; private set; }
    public string CurrencyCode { get; private set; } = default!;

    [Newtonsoft.Json.JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    private MoneyValue(int amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
        CurrencyCode = currency.ToString();
    }

    public static MoneyValue Create(int amount, Currency currency)
    {
        return new MoneyValue(amount, currency);
    }

    protected override IEnumerable<object> GetComponents()
    {
        yield return Amount;
        yield return Currency;
    }
}
