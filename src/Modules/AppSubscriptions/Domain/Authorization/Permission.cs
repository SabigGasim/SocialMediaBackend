namespace SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;

public enum Permissions
{
    CreateAppPlanProduct, CreateAppPlan, SubscribeToAppPlan,
    UnsubscribeFromAppPlan
}

public class Permission
{
    public static readonly Permission CreateAppPlanProduct = new(Permissions.CreateAppPlanProduct, "Permissions.AppPlan.CreateProduct");
    public static readonly Permission CreateAppPlan = new(Permissions.CreateAppPlan, "Permissions.AppPlan.CreatePlan");
    public static readonly Permission SubscribeToAppPlan = new(Permissions.SubscribeToAppPlan, "Permissions.AppPlan.Subscribe");
    public static readonly Permission UnsbscribeFromAppPlan = new(Permissions.UnsubscribeFromAppPlan, "Permissions.AppPlan.Unsubscribe");

    public Permissions Id { get; private set; }
    public string Name { get; private set; } = default!;

    private Permission() {}
    public Permission(Permissions permissionId, string name)
    {
        Id = permissionId;
        Name = name;
    }
}
