using Dapper;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Chat.Domain.Chatters;
using SocialMediaBackend.Modules.Chat.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Domain.Chatters;

internal class ChatterRepository(IDbConnectionFactory factory) : IChatterRepository
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<bool> ExistsAsync(ChatterId chatterId, CancellationToken ct = default)
    {
        const string sql = $"""
                SELECT EXISTS (
                    SELECT 1
                    FROM {Schema.Chat}."Chatters"
                    WHERE "Id" = @Id
                );
                """;

        using (var connection = await _factory.CreateAsync(ct))
        {
            return await connection.ExecuteScalarAsync<bool>(new CommandDefinition(
                sql, new
                { 
                    Id = chatterId.Value 
                },
                cancellationToken: ct));
        }
    }

    public async Task<ChatterDto?> GetByIdAsync(ChatterId chatterId, CancellationToken ct = default)
    {
        using (var connection = await _factory.CreateAsync(ct)) 
        {
            const string sql = $"""
                SELECT * FROM {Schema.Chat}."Chatters"
                WHERE "Id" = @Id
                """;
                
            return await connection.QuerySingleOrDefaultAsync<ChatterDto>(new CommandDefinition(sql, new
            {
                Id = chatterId.Value
            }, cancellationToken: ct));
        }
    }

    public async Task SetOnlineStatus(ChatterId chatterId, bool status)
    {
        const string sql = $"""
            UPDATE {Schema.Chat}."Chatters"
            SET "IsOnline" = FALSE
            WHERE "Id" = @Id;
            """;

        using (var connection = await _factory.CreateAsync())
        {
            await connection.ExecuteAsync(sql, new { Id = chatterId.Value });
        }
    }
}
