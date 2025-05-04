using System.Data;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Data;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateAsync(CancellationToken token = default);
}