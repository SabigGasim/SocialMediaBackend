using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Payments.Contracts.Gateway;
using SocialMediaBackend.Modules.Payments.Domain.Products;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.UpgradeSubscription;

public sealed class UpgradeSubscriptionCommand(
    Guid subscriptionId,
    Guid priceId) : CommandBase<CreateCheckoutSessionResponse>
{
    public SubscriptionId SubscriptionId { get; } = new(subscriptionId);
    public ProductPriceId PriceId { get; } = new(priceId);
}
