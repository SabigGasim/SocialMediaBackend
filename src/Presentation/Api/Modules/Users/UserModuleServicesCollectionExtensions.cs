using SocialMediaBackend.Modules.Users.Application;
using SocialMediaBackend.Modules.Users.Infrastructure;

namespace SocialMediaBackend.Api.Modules.Users;

public static class UserModuleServicesCollectionExtensions
{
    public static IServiceCollection AddUserModule(this IServiceCollection services)
    {
        return services
            .AddApplication()
            .AddInfrastructure();
    }
}
