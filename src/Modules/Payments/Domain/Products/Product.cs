using SocialMediaBackend.BuildingBlocks.Domain.EventSourcing;
using SocialMediaBackend.Modules.Payments.Contracts;
using SocialMediaBackend.Modules.Payments.Domain.Products.Events;

namespace SocialMediaBackend.Modules.Payments.Domain.Products;

public class Product : AggregateRoot
{
    public string Reference { get; private set; } = default!;
    public List<ProductPlan> Prices { get; private set; } = [];
    public string Owner { get; private set; } = default!;
    public string GatewayProductId { get; private set; } = default!;
    public bool IsDeleted { get; private set; }

    private Product() { }

    public static Product Create(
        string reference,
        string gatewayProductId,
        string owner)
    {
        var product = new Product();

        var @event = new ProductCreated(ProductId.New(), reference, gatewayProductId, owner);

        product.Apply(@event);
        product.AddEvent(@event);

        return product;
    }

    public ProductPriceId AddPrice(string gatewayId, ProductPrice price)
    {
        var priceId = ProductPriceId.New();

        var @event = new PriceAdded(priceId, gatewayId, price);

        this.Apply(@event);
        this.AddEvent(@event);

        return priceId;
    }

    public void MarkAsDeleted()
    {
        if (this.IsDeleted)
        {
            return;
        }

        var @event = new ProductDeleted();

        this.Apply(@event);
        this.AddEvent(@event);
    }

    public void ChangePrice(ProductPriceId priceId, ProductPrice newPrice)
    {
        var price = this.Prices.Find(p => p.Id == priceId);
        if (price == default || price.Price.Equals(newPrice))
        {
            return;
        }

        var @event = new PriceChanged(priceId, newPrice);

        this.Apply(@event);
        this.AddEvent(@event);
    }

    public void Apply(ProductCreated @event)
    {
        this.Id = @event.ProductId.Value;
        this.Reference = @event.Reference;
        this.GatewayProductId = @event.GatewayProductId;
        this.Owner = @event.Owner;
        this.IsDeleted = false;
    }

    public void Apply(ProductDeleted @event)
    {
        this.IsDeleted = true;
    }

    public void Apply(PriceAdded @event)
    {
        Prices.Add(new ProductPlan(@event.PriceId, @event.GatewayPriceId, @event.Price));
    }

    public void Apply(PriceChanged @event)
    {
        var price = this.Prices.First(p => p.Id == @event.PriceId);

        price = price with { Price = @event.NewPrice };
    }
}
