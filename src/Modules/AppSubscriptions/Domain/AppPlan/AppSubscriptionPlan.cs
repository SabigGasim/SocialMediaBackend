using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Contracts;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;

public class AppSubscriptionPlan : Entity<AppSubscriptionPlanId>
{
    public ProductPrice Price { get; init; } = default!;

    private AppSubscriptionPlan() { }
    public AppSubscriptionPlan(AppSubscriptionPlanId priceId, ProductPrice price)
    {
        Id = priceId;
        Price = price;
    }

    internal static AppSubscriptionPlan Create(AppSubscriptionPlanId planId, ProductPrice price)
    {
        return new AppSubscriptionPlan(planId, price);
    }
}
