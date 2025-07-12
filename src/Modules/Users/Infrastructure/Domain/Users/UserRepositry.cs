using Dapper;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Domain.Users;

public class UserRepositry(IDbConnectionFactory dbConnectionFactory) : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<bool> ExistsAsync(Guid userId, CancellationToken token = default)
    {
        const string sql = $"""
            SELECT EXISTS (
                SELECT 1 FROM {Schema.Users}."Users" WHERE "Id" = @{nameof(userId)}
            );
            """;

        using (var connection = await _dbConnectionFactory.CreateAsync(token))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(sql,
                new {userId},
                cancellationToken: token)
                );
        }
    }

    public async Task<bool> ExistsAsync(string username, CancellationToken token = default)
    {
        const string sql = $"""
            SELECT EXISTS (
                SELECT 1 FROM {Schema.Users}."Users" WHERE "Username" = @Username
            );
            """;

        using (var connection = await _dbConnectionFactory.CreateAsync(token))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(sql,
                new { Username = username },
                cancellationToken: token)
                );
        }
    }
}
