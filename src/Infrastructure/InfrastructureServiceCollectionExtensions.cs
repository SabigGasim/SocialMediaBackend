using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Infrastructure.Data;
using SocialMediaBackend.Infrastructure.Domain.Posts;
using SocialMediaBackend.Infrastructure.Domain.Users;

namespace SocialMediaBackend.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        return services
            .AddSingleton<IDbConnectionFactory>(new NpgsqlConnectionFactory(connectionString))
            .AddSingleton<IUserRepository, UserRepositry>()
            .AddSingleton<IPostRepository, PostRepository>()
            .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
    }
}
