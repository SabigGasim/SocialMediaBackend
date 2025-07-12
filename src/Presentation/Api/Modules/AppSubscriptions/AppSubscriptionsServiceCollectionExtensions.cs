using SocialMediaBackend.Modules.AppSubscriptions.Application.Contracts;

namespace SocialMediaBackend.Api.Modules.AppSubscriptions;

public static class AppSubscriptionsServiceCollectionExtensions
{
    public static IServiceCollection AddAppSubscriptionsModule(this IServiceCollection services)
    {
        return services.AddSingleton<IAppSubscriptionsModule, AppSubscriptionsModule>();
    }
}
