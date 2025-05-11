using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Persistence;

public class PersistenceModule(IConfiguration config) : Module
{
    private readonly IConfiguration _config = config;

    protected override void Load(ContainerBuilder builder)
    {
        var connectionString = _config.GetConnectionString("PostgresConnection");

        builder.Register(_ =>
        {
            var options = new DbContextOptionsBuilder<FeedDbContext>()
                .UseNpgsql(
                    connectionString,
                    npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(
                        HistoryRepository.DefaultTableName,
                        Schema.Feed))
                .Options;

            return new FeedDbContext(options);
        })
            .AsSelf()
            .As<DbContext>()
            .InstancePerLifetimeScope();
    }
}
