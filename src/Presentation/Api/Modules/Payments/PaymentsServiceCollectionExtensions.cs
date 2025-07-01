using SocialMediaBackend.Modules.Payments.Application;

namespace SocialMediaBackend.Api.Modules.Payments;

public static class PaymentsServiceCollectionExtensions
{
    public static IServiceCollection AddPaymentsModule(this IServiceCollection services)
    {
        return services.AddApplication();
    }
}