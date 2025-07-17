using Dapper;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Security;

public sealed class PermissionManager(IDbConnectionFactory factory) : IPermissionManager
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<bool> UserHasPermission(Guid userId, int permissionId, CancellationToken ct = default)
    {
        const string sql = $"""
            SELECT EXISTS (
                SELECT 1
                FROM {Schema.Feed}."AuthorRoles" ur
                JOIN {Schema.Feed}."RolePermissions" rp ON ur."RoleId" = rp."RoleId"
                WHERE ur."AuthorId" = @AuthorId
                  AND rp."PermissionId" = @PermissionId
            );
        """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(sql, new 
            { 
                AuthorId = userId, 
                PermissionId = permissionId
            }, cancellationToken: ct));
        }
    }

    public async Task<bool> UserHasPermissions(Guid userId, IEnumerable<int> permissionIds, CancellationToken ct = default)
    {
        const string sql = $"""
            WITH user_permissions AS (
                SELECT DISTINCT rp."PermissionId"
                FROM {Schema.Feed}."AuthorRoles" ur
                JOIN {Schema.Feed}."RolePermissions" rp ON ur."RoleId" = rp."RoleId"
                WHERE ur."AuthorId" = @AuthorId
            )
            SELECT
                (SELECT COUNT(*) FROM unnest(@PermissionIds) p) = 
                (SELECT COUNT(*) FROM user_permissions up WHERE up."PermissionId" = ANY(@PermissionIds));
        """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(sql, new
            {
                AuthorId = userId,
                PermissionIds = permissionIds
            }, cancellationToken: ct));
        }
    }

    public async Task<bool> UserIsInRole(Guid userId, int roleId, CancellationToken ct = default)
    {
        const string sql = $"""
            SELECT EXISTS (
                SELECT 1
                FROM {Schema.Feed}."AuthorRoles" ur
                WHERE ur."AuthorId" = @AuthorId
                  AND ur."RoleId" = @RoleId
            );
        """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(sql, new
            {
                AuthorId = userId,
                RoleId = roleId
            }, cancellationToken: ct));
        }
    }
}
