using Dapper;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Users;

public class UserRepositry(IDbConnectionFactory dbConnectionFactory) : IUserRepository
{
    private readonly IDbConnectionFactory _dbConnectionFactory = dbConnectionFactory;

    public async Task<bool> ExistsAsync(Guid userId, CancellationToken token = default)
    {
        const string sql = $"""
            SELECT EXISTS (
                SELECT 1 FROM {Schema.AppSubscriptions}."Users" WHERE "Id" = @UserId
            );
            """;

        using (var connection = await _dbConnectionFactory.CreateAsync(token))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(sql, 
                new { UserId = userId }, 
                cancellationToken: token)
                );
        }
    }
}
