using SocialMediaBackend.Modules.Chat.Domain.Authorization;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Roles;

internal class RolePermission
{
    public Chat.Domain.Authorization.Roles RoleId { get; private set; } = default!;
    public Permissions PermissionId { get; private set; } = default!;

    public Role Role { get; private set; } = default!;
    public Permission Permission { get; private set; } = default!;

    private RolePermission() { }
    public RolePermission(Chat.Domain.Authorization.Roles roleId, Permissions permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }
}