using Autofac;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.Modules.Feed.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Persistence;

public class PersistenceModule(string connectionString) : Module
{
    private readonly string _connectionString = connectionString;

    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(_ => new FeedDbContext(
            FeedDbContextOptionsBuilderFactory
                .Create(_connectionString)
                .Options))
            .AsSelf()
            .As<DbContext>()
            .InstancePerLifetimeScope();

        builder.Register(_ => new NpgsqlConnectionFactory(_connectionString))
            .As<IDbConnectionFactory>()
            .SingleInstance();
    }
}
