using System.Data;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Data;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync(CancellationToken token = default);
}