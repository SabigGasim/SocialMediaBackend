using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Contracts;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.AppPlans.CreateAppSubscriptionProduct;

[HasPermission(Permissions.CreateAppPlanProduct)]
public sealed class CreateAppSubscriptionProductCommand : CommandBase, IRequireAuthorization
{
    public AppSubscriptionTier Tier { get; }

    public CreateAppSubscriptionProductCommand(string tier)
    {
        Tier = Enum.Parse<AppSubscriptionTier>(tier, ignoreCase: true);
    }
}
