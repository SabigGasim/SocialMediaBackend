using JasperFx;
using JasperFx.Events.Projections;
using Marten;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaBackend.BuildingBlocks.Domain.Serialization;
using SocialMediaBackend.Modules.Payments.Domain.Payers;
using SocialMediaBackend.Modules.Payments.Domain.Products;
using SocialMediaBackend.Modules.Payments.Domain.Subscriptions;
using SocialMediaBackend.Modules.Payments.Infrastructure.Domain.Payments;
using SocialMediaBackend.Modules.Payments.Infrastructure.Domain.Products;
using SocialMediaBackend.Modules.Payments.Infrastructure.Domain.Subscriptions;

namespace SocialMediaBackend.Modules.Payments.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IWebHostEnvironment environment,
        string connectionString)
    {
        services.AddMarten(options =>
        {
            options.Connection(connectionString);
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

            if (!environment.IsProduction())
            {
                options.AutoCreateSchemaObjects = AutoCreate.All;
            }
        })
            .UseLightweightSessions();

        return services;
    }
}
