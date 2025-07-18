﻿using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Contracts;
using SocialMediaBackend.Modules.Payments.Contracts;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;

public class AppSubscriptionProduct : AggregateRoot<AppSubscriptionProductId>
{
    private readonly List<AppSubscriptionPlan> _plans = new();

    public AppSubscriptionTier Tier { get; }
    public IReadOnlyCollection<AppSubscriptionPlan> Plans => _plans.AsReadOnly();
    public static string Reference => $"app_plan";

    private AppSubscriptionProduct() { }
    private AppSubscriptionProduct(AppSubscriptionProductId productId, AppSubscriptionTier tier)
    {
        Id = productId;
        Tier = tier;
    }

    public static AppSubscriptionProduct Create(AppSubscriptionProductId productId, AppSubscriptionTier tier)
    {
        return new AppSubscriptionProduct(productId, tier);
    }

    public AppSubscriptionPlan AddPlan(AppSubscriptionPlanId planId, ProductPrice price)
    {
        var plan = AppSubscriptionPlan.Create(this.Id, planId, price);

        _plans.Add(plan);

        return plan;
    }

    public static (string Name, string Description) GetProductDetails(AppSubscriptionTier tier)
    {
        return (GetProductName(tier), GetDescription(tier));
    }

    public static string GetProductName(AppSubscriptionTier tier) => $"App Plan - {tier}";
    public static string GetDescription(AppSubscriptionTier tier)
    {
        return $"Get access to all {tier.ToString().ToLower()} tier features.";
    }
}
