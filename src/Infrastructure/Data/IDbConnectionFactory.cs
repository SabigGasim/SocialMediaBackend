using System.Data;

namespace SocialMediaBackend.Infrastructure.Data;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync(CancellationToken token = default);
}