using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureBuildingBlocks(
        this IServiceCollection services,
        IConfiguration config)
    {
        var connectionString = config.GetConnectionString("PostgresConnection")!;

        return services.AddSingleton<IDbConnectionFactory>(new NpgsqlConnectionFactory(connectionString));
    }
}
