using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;

namespace SocialMediaBackend.BuildingBlocks.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureBuildingBlocks(this IServiceCollection services)
    {
        return services.AddHostedService<InMemoryEventBusBackgroundService>();
    }
}
