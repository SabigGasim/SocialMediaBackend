using Autofac;

namespace SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;

public static class ChatCompositionRoot
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
