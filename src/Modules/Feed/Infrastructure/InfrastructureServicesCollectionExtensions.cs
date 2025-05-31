using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Authors;
using SocialMediaBackend.Modules.Feed.Infrastructure.Domain.Posts;

namespace SocialMediaBackend.Modules.Feed.Infrastructure;

public static class InfrastructureServicesCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddSingleton<IPostRepository, PostRepository>()
            .AddSingleton<IAuthorRepository, AuthorRepository>();
    }
}
