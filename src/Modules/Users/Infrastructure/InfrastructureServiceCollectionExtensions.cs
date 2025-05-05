using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;
using SocialMediaBackend.Modules.Users.Infrastructure.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Processing;

namespace SocialMediaBackend.Modules.Users.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        return services
            .AddSingleton<IDbConnectionFactory>(new NpgsqlConnectionFactory(connectionString))
            .AddSingleton<IUserRepository, UserRepositry>()
            .AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>()
            .AddDbContext<UsersDbContext>(options =>
            {
                options.UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        Schema.Users));
            })
            .AddScoped<IUnitOfWork, UnitOfWork<UsersDbContext>>();
    }
}
