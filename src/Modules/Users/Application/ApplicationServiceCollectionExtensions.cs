using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Users.Application.Auth;
using SocialMediaBackend.Modules.Users.Application.Contracts;
using SocialMediaBackend.Modules.Users.Application.DomainServices.Users;
using SocialMediaBackend.Modules.Users.Domain.Services;

namespace SocialMediaBackend.Modules.Users.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddSingleton<IUserExistsChecker, UserExistsChecker>()
            .AddSingleton<IAuthorizationService, AuthorizationService>()
            .AddSingleton<IUsersModule, UsersModule>()
            ;
    }
}
