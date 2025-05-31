using Dapper;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Feed.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Authors;

public class AuthorRepository(IDbConnectionFactory factory) : IAuthorRepository
{
    private readonly IDbConnectionFactory _factory = factory;

    public async Task<AuthorDto?> GetByIdAsync(AuthorId authorId, CancellationToken ct = default)
    {
        using (var connection = await _factory.CreateAsync(ct))
        {
            const string sql = $"""
                SELECT * FROM {Schema.Feed}."Authors"
                WHERE "Id" = @Id
                """;

            return await connection.QuerySingleOrDefaultAsync<AuthorDto>(new CommandDefinition(sql, new 
            { 
                Id = authorId.Value
            }, cancellationToken: ct));
        }
    }
}
