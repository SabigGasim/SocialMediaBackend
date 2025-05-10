using Npgsql;
using System.Data;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public class NpgsqlConnectionFactory(string connectionString) : IDbConnectionFactory
{
    private readonly string _connectionString = connectionString;

    public async Task<IDbConnection> CreateAsync(CancellationToken token = default)
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(token);
        return connection;
    }
}
