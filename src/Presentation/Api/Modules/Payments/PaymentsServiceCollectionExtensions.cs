using SocialMediaBackend.Modules.Payments.Application;
using SocialMediaBackend.Modules.Payments.Infrastructure;

namespace SocialMediaBackend.Api.Modules.Payments;

public static class PaymentsServiceCollectionExtensions
{
    public static IServiceCollection AddPaymentsModule(this IServiceCollection services)
    {
        return services.AddApplication();
    }
}