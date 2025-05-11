using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Infrastructure.Domain.Users;

namespace SocialMediaBackend.Modules.Users.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services.AddSingleton<IUserRepository, UserRepositry>();
    }
}
