using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.Payments.Application.Subscriptions.ReactivateSubscription;

public sealed class ReactivateSubscriptionCommand(Guid subscriptionId) : CommandBase
{
    public SubscriptionId SubscriptionId { get; } = new(subscriptionId);
}
