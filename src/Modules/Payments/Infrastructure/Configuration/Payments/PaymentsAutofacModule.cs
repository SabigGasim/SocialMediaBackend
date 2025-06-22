using Autofac;
using SocialMediaBackend.Modules.Payments.Infrastructure.Gateway;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Configuration.Payments;

public class PaymentsAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<StripePaymentService>()
            .As<IPaymentService>()
            .SingleInstance();

        builder.RegisterType<StripeGateway>()
            .As<IPaymentGateway>()
            .SingleInstance();
    }
}
