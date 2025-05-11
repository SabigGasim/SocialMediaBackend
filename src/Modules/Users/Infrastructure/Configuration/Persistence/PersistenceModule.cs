using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Persistence;

public class PersistenceModule(IConfiguration config) : Module
{
    private readonly IConfiguration _config = config;

    protected override void Load(ContainerBuilder builder)
    {
        var connectionString = _config.GetConnectionString("PostgresConnection");

        builder.Register(_ =>
        {
            var options = new DbContextOptionsBuilder<UsersDbContext>()
                .UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        Schema.Users))
                .Options;

            return new UsersDbContext(options);
        })
            .AsSelf()
            .As<DbContext>()
            .InstancePerLifetimeScope();
    }
}
