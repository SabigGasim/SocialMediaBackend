using SocialMediaBackend.Modules.Feed.Application;
using SocialMediaBackend.Modules.Feed.Infrastructure;

namespace SocialMediaBackend.Api.Modules.Feed;

internal static class FeedModuleServicesCollectionExtensions
{
    public static IServiceCollection AddFeedModule(this IServiceCollection services)
    {
        return services
            .AddApplication()
            .AddInfrastructure();
    }
}
