using Dapper;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Security;

public sealed class PermissionManager(IDbConnectionFactory factory) : IPermissionManager
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<bool> UserHasPermission(Guid userId, int permissionId, CancellationToken ct = default)
    {
        const string sql = $"""
            SELECT EXISTS (
                SELECT 1
                FROM {Schema.Users}."UserRoles" ur
                JOIN {Schema.Users}."RolePermissions" rp ON ur."RoleId" = rp."RoleId"
                WHERE ur."UserId" = @UserId
                  AND rp."PermissionId" = @PermissionId
            );
        """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(sql, new 
            { 
                UserId = userId, 
                PermissionId = permissionId
            }, cancellationToken: ct));
        }
    }

    public async Task<bool> UserHasPermissions(Guid userId, IEnumerable<int> permissionIds, CancellationToken ct = default)
    {
        const string sql = $"""
            WITH user_permissions AS (
                SELECT DISTINCT rp."PermissionId"
                FROM {Schema.Users}."UserRoles" ur
                JOIN {Schema.Users}."RolePermissions" rp ON ur."RoleId" = rp."RoleId"
                WHERE ur."UserId" = @UserId
            )
            SELECT
                (SELECT COUNT(*) FROM unnest(@PermissionIds) p) = 
                (SELECT COUNT(*) FROM user_permissions up WHERE up."PermissionId" = ANY(@PermissionIds));
        """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(sql, new
            {
                UserId = userId,
                PermissionIds = permissionIds
            }, cancellationToken: ct));
        }
    }
}
