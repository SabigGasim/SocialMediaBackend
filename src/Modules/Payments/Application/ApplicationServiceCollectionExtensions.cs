using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Payments.Application.Contracts;
using SocialMediaBackend.Modules.Payments.Contracts.Proxy;

namespace SocialMediaBackend.Modules.Payments.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddSingleton<IPaymentsModule, PaymentsModule>()
            .AddSingleton<IPaymentAntiCorruptionLayer, PaymentAntiCorruptionLayer>();
    }
}
