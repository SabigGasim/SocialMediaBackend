using Autofac;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Configuration;

public static class AppSubscriptionsCompositionRoot
{
    private static IContainer _container = default!;

    public static void SetContainer(IContainer container)
    {
        _container = container;
    }

    public static ILifetimeScope BeginLifetimeScope()
    {
        return _container.BeginLifetimeScope();
    }
}
