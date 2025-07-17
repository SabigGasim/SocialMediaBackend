namespace SocialMediaBackend.BuildingBlocks.Application.Auth;

public interface IPermissionManager
{
    Task<bool> UserHasPermission(Guid userId, int permissionId, CancellationToken ct = default);
    Task<bool> UserIsInRole(Guid userId, int roleId, CancellationToken ct = default);
    Task<bool> UserHasPermissions(Guid userId, IEnumerable<int> permissionIds, CancellationToken ct = default);
}
