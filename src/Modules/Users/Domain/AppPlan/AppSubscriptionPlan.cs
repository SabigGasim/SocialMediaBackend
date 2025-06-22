using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.Payments.Contracts;

namespace SocialMediaBackend.Modules.Users.Domain.AppPlan;

public class AppSubscriptionPlan : Entity<AppSubscriptionPlanId>
{
    public ProductPrice Price { get; init; } = default!;

    private AppSubscriptionPlan() { }
    public AppSubscriptionPlan(AppSubscriptionPlanId priceId, ProductPrice price)
    {
        this.Id = priceId;
        this.Price = price;
    }

    internal static AppSubscriptionPlan Create(AppSubscriptionPlanId planId, ProductPrice price)
    {
        return new AppSubscriptionPlan(planId, price);
    }
}
