using SocialMediaBackend.BuildingBlocks.Domain;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;

public interface ISubscriptionService
{
    Task<Result> CanIssueSubscriptionPaymentIntentAsync(
        UserId userId,
        CancellationToken ct = default);
}
