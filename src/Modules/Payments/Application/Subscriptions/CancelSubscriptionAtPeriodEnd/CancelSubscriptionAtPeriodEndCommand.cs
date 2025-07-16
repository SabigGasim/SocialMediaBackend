using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.CancelSubscriptionAtPeriodEnd;

public sealed class CancelSubscriptionAtPeriodEndCommand(Guid subscriptionId) : CommandBase
{
    public SubscriptionId SubscriptionId { get; } = new(subscriptionId);
}
