using Autofac;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Subscriptions;
using SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration.AppSubscriptions;

public class AppSubscriptionsAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UserRepositry>()
            .As<IUserRepository>()
            .SingleInstance();

        builder.RegisterType<SubscriptionService>()
            .As<ISubscriptionService>()
            .SingleInstance();
    }
}
