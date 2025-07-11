namespace SocialMediaBackend.Modules.Users.Domain.Authorization;

public static class RolePermissionData
{
    private static readonly Dictionary<Role, Permission[]> _mappings;

    public static IReadOnlyDictionary<Role, Permission[]> Mappings => _mappings.AsReadOnly();

    static RolePermissionData()
    {
        _mappings = [];
        
        _mappings[Role.User] = [
            Permission.GetUsers,
            Permission.GetFullUserDetails,
            Permission.DeleteCurrentUser,
            Permission.ModifyUserInfo,
            Permission.FollowUsers,
            Permission.AcceptFollowRequests,
            Permission.RejectFollowRequests,
            Permission.UnfollowUsers,
            Permission.SubscribeToAppPlan,
            Permission.UnsbscribeFromAppPlan
        ];

        _mappings[Role.BasicPlan] = [];
        _mappings[Role.PlusPlan] = [.. _mappings[Role.BasicPlan]];

        _mappings[Role.AdminUser] = new HashSet<Permission>(
            [
                Permission.CreateAppPlan,
                Permission.CreateAppPlanProduct,
                Permission.DeleteUsers,
                .._mappings[Role.User]
                    .Concat(_mappings[Role.BasicPlan])
                    .Concat(_mappings[Role.PlusPlan])
            ]
        )
            .ToArray();
    }
}
