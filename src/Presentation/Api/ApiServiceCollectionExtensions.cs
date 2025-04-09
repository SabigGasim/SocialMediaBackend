using FastEndpoints;
using FastEndpoints.Swagger;

namespace SocialMediaBackend.Api;

public static class ApiServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        return services
            .AddFastEndpoints()
            .SwaggerDocument()
            ;
    }   
}
