using SocialMediaBackend.BuildingBlocks.Infrastructure;

namespace SocialMediaBackend.Api.Modules.BuildingBlocks;

internal static class BuildingBlocksServiceCollectionExtensions
{
    public static IServiceCollection AddBuildingBlocks(this IServiceCollection services)
    {
        return services.AddInfrastructureBuildingBlocks();
    }
}
