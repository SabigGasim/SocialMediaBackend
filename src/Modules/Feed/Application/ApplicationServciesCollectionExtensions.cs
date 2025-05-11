using Microsoft.Extensions.DependencyInjection;

namespace SocialMediaBackend.Modules.Feed.Application;

public static class ApplicationServciesCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
