using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Infrastructure.Data;

namespace SocialMediaBackend.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("App");
            });
    }
}
