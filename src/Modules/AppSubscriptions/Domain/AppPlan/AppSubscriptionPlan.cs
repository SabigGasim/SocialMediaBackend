using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Contracts;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;

public class AppSubscriptionPlan : Entity<AppSubscriptionPlanId>
{
    public ProductPrice Price { get; init; } = default!;
    public AppSubscriptionProductId AppSubscriptionProductId { get; private set; } = default!;
    public AppSubscriptionProduct AppSubscriptionProduct { get; private set; } = default!;

    private AppSubscriptionPlan() { }
    public AppSubscriptionPlan(
        AppSubscriptionProductId productId, 
        AppSubscriptionPlanId priceId, 
        ProductPrice price)
    {
        Id = priceId;
        Price = price;
        AppSubscriptionProductId = productId;
    }

    internal static AppSubscriptionPlan Create(
        AppSubscriptionProductId productId,
        AppSubscriptionPlanId planId, 
        ProductPrice price)
    {
        return new AppSubscriptionPlan(productId, planId, price);
    }
}
