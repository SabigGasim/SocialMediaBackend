namespace SocialMediaBackend.Api;

public static class ApiServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        return services
            .AddHttpContextAccessor()
            .AddMediator(options => options.ServiceLifetime = ServiceLifetime.Transient)
            ;
    }
}
