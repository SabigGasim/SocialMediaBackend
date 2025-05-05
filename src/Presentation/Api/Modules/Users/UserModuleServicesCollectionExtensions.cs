using SocialMediaBackend.Modules.Users.Application;
using SocialMediaBackend.Modules.Users.Infrastructure;

namespace SocialMediaBackend.Api.Modules.Users;

public static class UserModuleServicesCollectionExtensions
{
    public static IServiceCollection AddUserModule(this IServiceCollection services, IConfiguration config)
    {
        return services
            .AddApplication()
            .AddInfrastructure(config.GetConnectionString("PostgresConnection")!);
    }
}
