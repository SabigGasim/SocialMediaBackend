using Autofac;
using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.Modules.Payments.Infrastructure.Data;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.Persistence;

public class PersistenceModule(string connectionString) : Module
{
    private readonly string _connectionString = connectionString;

    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(_ => new PaymentsDbContext(
            PaymentsDbContextOptionsBuilderFactory
                .Create(_connectionString)
                .Options))
            .AsSelf()
            .As<DbContext>()
            .InstancePerLifetimeScope();

        builder.Register(_ => new NpgsqlConnectionFactory(_connectionString))
            .As<IDbConnectionFactory>()
            .SingleInstance();

        builder.RegisterType<MartenAggregateRepository>()
            .As<IAggregateRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<AggregateTracker>()
            .As<IAggregateTracker>()
            .InstancePerLifetimeScope();
    }
}
