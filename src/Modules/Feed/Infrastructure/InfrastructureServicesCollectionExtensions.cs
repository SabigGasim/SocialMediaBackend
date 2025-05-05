using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;
using SocialMediaBackend.Modules.Users.Infrastructure.Processing;

namespace SocialMediaBackend.Modules.Feed.Infrastructure;

public static class InfrastructureServicesCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        return services
            .AddSingleton<IDbConnectionFactory>(new NpgsqlConnectionFactory(connectionString))
            .AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>()
            .AddDbContext<FeedDbContext>(options =>
            {
                options.UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        Schema.Feed));
            })
            .AddScoped<IUnitOfWork, UnitOfWork<FeedDbContext>>();
    }
}
