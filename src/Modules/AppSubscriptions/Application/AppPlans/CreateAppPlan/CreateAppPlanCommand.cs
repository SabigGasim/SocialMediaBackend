using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Contracts;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;
using SocialMediaBackend.Modules.Payments.Contracts;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.AppPlans.CreateAppPlan;

[HasPermission(Permissions.CreateAppPlan)]
public sealed class CreateAppPlanCommand : CommandBase, IRequireAuthorization
{
    public CreateAppPlanCommand(IEnumerable<PriceRequest> prices, string tier)
    {
        Prices = prices.Select(x =>
        {
            var currency = Enum.Parse<Currency>(x.Currency, ignoreCase: true);
            var interval = Enum.Parse<PaymentInterval>(x.Interval, ignoreCase: true);

            return new ProductPrice(new(x.Amount, currency), interval);
        })
            .ToArray();

        Tier = Enum.Parse<AppSubscriptionTier>(tier, ignoreCase: true);
    }

    public ProductPrice[] Prices { get; private set; }
    public AppSubscriptionTier Tier { get; private set; }
}
