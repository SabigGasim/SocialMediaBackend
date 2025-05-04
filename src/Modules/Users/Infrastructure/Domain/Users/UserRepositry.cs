using Dapper;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Domain.Users;

public class UserRepositry : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory;

    public UserRepositry(IDbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<bool> ExistsAsync(Guid userId, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateAsync();
        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition($"""
            SELECT EXISTS (
                SELECT 1 FROM "Users" WHERE "Id" = @{nameof(userId)}
            );
            """, new {userId}, cancellationToken: token));
    }

    public async Task<bool> ExistsAsync(string username, CancellationToken token = default)
    {
        using var connection = await _dbConnectionFactory.CreateAsync(token);
        return await connection.ExecuteScalarAsync<bool>(new CommandDefinition($"""
            SELECT COUNT(1) FROM "Users" WHERE "Username" = @{nameof(username)}
            """, new { username }, cancellationToken: token));
    }
}
