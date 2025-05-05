using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Feed.Application.Auth;

namespace SocialMediaBackend.Modules.Feed.Application;

public static class ApplicationServciesCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAuthorizationService, AuthorizationService>()
            .AddMediator(o =>
            {
                o.ServiceLifetime = ServiceLifetime.Scoped;
                o.Namespace = "SocialMediaBackend.Modules.Feed.Application.SourceGenerated.Mediator";
            })
            ;
    }
}
