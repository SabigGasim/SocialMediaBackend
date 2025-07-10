using SocialMediaBackend.Modules.Feed.Domain.Authorization;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Roles;

internal class RolePermission
{
    public Feed.Domain.Authorization.Roles RoleId { get; private set; } = default!;
    public Feed.Domain.Authorization.Permissions PermissionId { get; private set; } = default!;

    public Role Role { get; private set; } = default!;
    public Permission Permission { get; private set; } = default!;

    private RolePermission() { }
    public RolePermission(Feed.Domain.Authorization.Roles roleId, Feed.Domain.Authorization.Permissions permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }
}