namespace SocialMediaBackend.Modules.Users.Domain.Authorization;

public enum Permissions
{
    GetFullUserDetails, GetUsers, ModifyUserInfo,
    FollowUsers, AcceptFollowRequests, RejectFollowRequests, 
    UnfollowUsers, DeleteCurrentUser, DeleteUsers,
    CreateAppPlanProduct, CreateAppPlan, SubscribeToAppPlan,
    UnsubscribeFromAppPlan
}

public class Permission
{
    public static readonly Permission GetFullUserDetails = new(Permissions.GetFullUserDetails, "Permissions.Users.GetFullDetails");
    public static readonly Permission GetUsers = new(Permissions.GetUsers, "Permissions.Users.Get");
    public static readonly Permission ModifyUserInfo = new(Permissions.ModifyUserInfo, "Permissions.Users.ModifyInfo");
    public static readonly Permission FollowUsers = new(Permissions.FollowUsers, "Permissions.Users.Follow");
    public static readonly Permission AcceptFollowRequests = new(Permissions.AcceptFollowRequests, "Permissions.Users.AcceptFollowRequests");
    public static readonly Permission RejectFollowRequests = new(Permissions.RejectFollowRequests, "Permissions.Users.RejectFollowRequests");
    public static readonly Permission UnfollowUsers = new(Permissions.UnfollowUsers, "Permissions.Users.Unfollow");
    public static readonly Permission DeleteCurrentUser = new(Permissions.DeleteCurrentUser, "Permissions.Users.DeleteSelf");
    public static readonly Permission DeleteUsers = new(Permissions.DeleteUsers, "Permissions.Users.Delete");
    public static readonly Permission CreateAppPlan = new(Permissions.SubscribeToAppPlan, "Permissions.AppPlan.CreatePlan");
    public static readonly Permission CreateAppPlanProduct = new(Permissions.SubscribeToAppPlan, "Permissions.AppPlan.CreateProduct");
    public static readonly Permission SubscribeToAppPlan = new(Permissions.SubscribeToAppPlan, "Permissions.AppPlan.Subscribe");
    public static readonly Permission UnsbscribeFromAppPlan = new(Permissions.SubscribeToAppPlan, "Permissions.AppPlan.Unsubscribe");

    public Permissions Id { get; private set; }
    public string Name { get; private set; } = default!;

    private Permission() {}
    public Permission(Permissions permissionId, string name)
    {
         this.Id = permissionId;
        this.Name = name;
    }
}
