using JasperFx;
using Marten;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            options.UseSystemTextJsonForSerialization();
            options.DatabaseSchemaName = Schema.Payments;

            if (!environment.IsProduction())
            {
                options.AutoCreateSchemaObjects = AutoCreate.All;
            }
        });

        return services;
    }
}
