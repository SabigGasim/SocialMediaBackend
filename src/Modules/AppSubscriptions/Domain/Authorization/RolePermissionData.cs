namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;

public static class RolePermissionData
{
    private static readonly Dictionary<Role, Permission[]> _mappings = [];

    public static IReadOnlyDictionary<Role, Permission[]> Mappings => _mappings.AsReadOnly();

    static RolePermissionData()
    {
        _mappings[Role.User] = [
            Permission.SubscribeToAppPlan,
            Permission.UnsbscribeFromAppPlan,
            Permission.RenewAppSubscription,
            Permission.CancelAppSubscriptionAtPeriodEnd,
            Permission.ReactivateAppSubscription
        ];

        _mappings[Role.AdminUser] = [
            Permission.CreateAppPlan,
            Permission.CreateAppPlanProduct,
            Permission.CancelAppSubscription,
            .._mappings[Role.User]
        ];
    }
}
