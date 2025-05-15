using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Feed.Application.Contracts;

namespace SocialMediaBackend.Modules.Feed.Application;

public static class ApplicationServcieCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services.AddSingleton<IFeedModule, FeedModule>();
    }
}
