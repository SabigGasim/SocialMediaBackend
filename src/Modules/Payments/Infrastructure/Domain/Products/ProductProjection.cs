using Marten.Events.Aggregation;
using SocialMediaBackend.Modules.Payments.Domain.Products;
using SocialMediaBackend.Modules.Payments.Domain.Products.Events;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Domain.Products;

internal class ProductProjection : SingleStreamProjection<Product, Guid>
{
    public void Apply(Product product, ProductCreated @event) => product.Apply(@event);
    public void Apply(Product product, PriceAdded @event) => product.Apply(@event);
    public void Apply(Product product, ProductDeleted @event) => product.Apply(@event);
    public void Apply(Product product, PriceChanged @event) => product.Apply(@event);
}
