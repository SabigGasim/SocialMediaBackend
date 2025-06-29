using Autofac;
using Autofac.Extensions.DependencyInjection;
using JasperFx;
using JasperFx.Events.Projections;
using Marten;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.BuildingBlocks.Domain.Serialization;
using SocialMediaBackend.BuildingBlocks.Infrastructure;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing;
using SocialMediaBackend.BuildingBlocks.Infrastructure.EventSourcing.Messaging;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.Products;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;
using SocialMediaBackend.Modules.Payments.Infrastructure.Domain.Payments;
using SocialMediaBackend.Modules.Payments.Infrastructure.Domain.Products;
using SocialMediaBackend.Modules.Payments.Infrastructure.Domain.Subscriptions;
using SocialMediaBackend.Modules.Payments.Infrastructure.InternalCommands;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.Persistence;

public class PersistenceModule(string connectionString, IHostEnvironment env) : Module
{
    private readonly string _connectionString = connectionString;
    private readonly IHostEnvironment _env = env;

    protected override void Load(ContainerBuilder builder)
    {
        builder.Register(_ => new NpgsqlConnectionFactory(_connectionString))
            .As<IDbConnectionFactory>()
            .SingleInstance();

        builder.RegisterType<MartenAggregateRepository>()
            .As<IAggregateRepository>()
            .InstancePerLifetimeScope();

        builder.RegisterType<AggregateTracker>()
            .As<IAggregateTracker>()
            .InstancePerLifetimeScope();

        var services = new ServiceCollection();

        services.AddMarten(options =>
        {
            options.Connection(_connectionString);
            options.UseNewtonsoftForSerialization(configure: options =>
            {
                options.ContractResolver = new NonPublicPropertySetterContractResolver();
            });
            options.DatabaseSchemaName = Schema.Payments;

            options.Schema.For<Payer>();
            options.Projections.Add<PayerProjection>(ProjectionLifecycle.Inline);

            options.Schema.For<Product>();
            options.Projections.Add<ProductProjection>(ProjectionLifecycle.Inline);

            options.Schema.For<Subscription>().ForeignKey<Payer>(x => x.PayerId);
            options.Projections.Add<SubscriptionProjection>(ProjectionLifecycle.Inline);

            options.Schema.For<InternalCommand>();
            options.Schema.For<InboxMessage>();
            options.Schema.For<OutboxMessage>();

            if (!_env.IsProduction())
            {
                options.AutoCreateSchemaObjects = AutoCreate.All;
            }
        })
            .UseLightweightSessions();

        builder.Populate(services);
    }
}
