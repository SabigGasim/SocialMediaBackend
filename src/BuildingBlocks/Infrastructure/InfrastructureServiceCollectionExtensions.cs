using Microsoft.Extensions.DependencyInjection;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureBuildingBlocks(
        this IServiceCollection services,
        string connectionString)
    {
        return services.AddSingleton<IDbConnectionFactory>(new NpgsqlConnectionFactory(connectionString));
    }
}
