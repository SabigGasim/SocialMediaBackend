namespace SocialMediaBackend.BuildingBlocks.Application.Auth;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
public sealed class HasPermissionAttribute : Attribute
{
    public int[] Permissions { get; }

    /// <param name="permissionIds">These are the enum Ids defined by the calling module</param>
    public HasPermissionAttribute(params object[] permissionIds)
    {
        Permissions = [..permissionIds.Select(x => (int)x)];
    }

    /// <param name="permissionId">This is the enum Id defnied by the calling module</param>
    public HasPermissionAttribute(object permissionId)
    {
        Permissions = [(int)permissionId];
    }
}