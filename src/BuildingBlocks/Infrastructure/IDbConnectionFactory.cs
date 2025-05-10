using System.Data;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync(CancellationToken token = default);
}