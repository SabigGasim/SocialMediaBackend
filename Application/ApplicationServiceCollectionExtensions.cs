using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Application.DomainServices.Users;
using SocialMediaBackend.Domain.Services;

namespace SocialMediaBackend.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddSingleton<IUserExistsChecker, UserExistsChecker>()
            ;
    }
}
