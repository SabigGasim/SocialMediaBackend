using Autofac;

namespace SocialMediaBackend.Modules.Feed.Infrastructure.Configuration;

public static class FeedCompositionRoot
{
    private static ILifetimeScope _container = default!;

    public static void SetContainer(ILifetimeScope container)
    {
        _container = container;
    }

    public static ILifetimeScope BeginLifetimeScope()
    {
        return _container.BeginLifetimeScope();
    }
}
