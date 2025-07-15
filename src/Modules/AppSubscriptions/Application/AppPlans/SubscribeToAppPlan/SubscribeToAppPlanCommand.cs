using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Application.Requests;
using SocialMediaBackend.BuildingBlocks.Application.Requests.Commands;
using SocialMediaBackend.Modules.AppSubscriptions.Contracts;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;
using SocialMediaBackend.Modules.Payments.Contracts;
using SocialMediaBackend.Modules.Payments.Contracts.Gateway;

namespace SocialMediaBackend.Modules.AppSubscriptions.Application.AppPlans.SubscribeToAppPlan;

[HasPermission(Permissions.SubscribeToAppPlan)]
public sealed class SubscribeToAppPlanCommand(string tier, string interval) 
    : CommandBase<CreateCheckoutSessionResponse>, IUserRequest
{
    public Guid UserId { get; private set; }
    public bool IsAdmin { get; private set; }
    public AppSubscriptionTier AppPlanTier { get; } = Enum.Parse<AppSubscriptionTier>(tier, ignoreCase: true);
    public PaymentInterval PaymentInterval { get; } = Enum.Parse<PaymentInterval>(interval, ignoreCase: true);

    public void WithAdminRole(bool isAdmin) => IsAdmin = isAdmin;
    public void WithUserId(Guid userId) => UserId = userId;
}
