using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.AppPlans.CreateAppSubscriptionProduct;

[HasPermission(Permissions.CreateAppPlanProduct)]
public sealed class CreateAppSubscriptionProductCommand : CommandBase, IUserRequest
{
    public AppSubscriptionTier Tier { get; }
    public Guid UserId { get; private set; }
    public bool IsAdmin { get; private set; }

    public CreateAppSubscriptionProductCommand(string tier)
    {
        Tier = Enum.Parse<AppSubscriptionTier>(tier, ignoreCase: true);
    }

    public void WithAdminRole(bool isAdmin) => IsAdmin = isAdmin;
    public void WithUserId(Guid userId) => UserId = userId;
}
