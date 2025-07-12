using SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;
using DomainAuth = SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;
namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Roles;

internal class RolePermission
{
    public DomainAuth.Roles RoleId { get; private set; } = default!;
    public DomainAuth.Permissions PermissionId { get; private set; } = default!;

    public Role Role { get; private set; } = default!;
    public Permission Permission { get; private set; } = default!;

    private RolePermission() { }
    public RolePermission(DomainAuth.Roles roleId, DomainAuth.Permissions permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }
}