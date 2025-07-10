namespace SocialMediaBackend.Modules.Users.Domain.Authorization;

public enum Permissions
{
    GetFullUserDetails, GetUsers, ModifyUserInfo,
    FollowUsers, AcceptFollowRequests, RejectFollowRequests, 
    DeleteCurrentUser, DeleteUsers,
}

public class Permission
{
    public static readonly Permission GetFullUserDetails = new(Permissions.GetFullUserDetails, "Permissions.Users.GetFullDetails");
    public static readonly Permission GetUsers = new(Permissions.GetUsers, "Permissions.Users.Get");
    public static readonly Permission ModifyUserInfo = new(Permissions.ModifyUserInfo, "Permissions.Users.ModifyInfo");
    public static readonly Permission FollowUsers = new(Permissions.FollowUsers, "Permissions.Users.Follow");
    public static readonly Permission AcceptFollowRequests = new(Permissions.AcceptFollowRequests, "Permissions.Users.AcceptFollowRequests");
    public static readonly Permission RejectFollowRequests = new(Permissions.RejectFollowRequests, "Permissions.Users.RejectFollowRequests");
    public static readonly Permission DeleteCurrentUser = new(Permissions.DeleteCurrentUser, "Permissions.Users.DeleteSelf");
    public static readonly Permission DeleteUsers = new(Permissions.DeleteUsers, "Permissions.Users.Delete");

    public Permissions Id { get; private set; }
    public string Name { get; private set; } = default!;

    private Permission() {}
    public Permission(Permissions permissionId, string name)
    {
         this.Id = permissionId;
        this.Name = name;
    }
}
