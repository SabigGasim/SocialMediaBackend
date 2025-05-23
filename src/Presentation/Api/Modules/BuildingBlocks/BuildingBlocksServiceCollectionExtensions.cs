﻿using SocialMediaBackend.BuildingBlocks.Infrastructure;

namespace SocialMediaBackend.Api.Modules.BuildingBlocks;

internal static class BuildingBlocksServiceCollectionExtensions
{
    public static IServiceCollection AddBuildingBlocks(
        this IServiceCollection services,
        string connectionString)
    {
        return services.AddInfrastructureBuildingBlocks(connectionString);
    }
}
