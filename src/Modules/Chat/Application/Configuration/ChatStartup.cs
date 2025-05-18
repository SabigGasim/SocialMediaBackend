using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Autofac.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Chat.Application.Configuration.Mediator;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Persistence;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Quartz;
using SocialMediaBackend.Modules.Chat.Infrastructure.Configuration.Processing;
using SocialMediaBackend.Modules.Chat.Application.Configuration.Auth;

namespace SocialMediaBackend.Modules.Chat.Application.Configuration;

public static class ChatStartup
{
    public static async Task InitializeAsync(
        IServiceCollection serviceCollection,
        IConfiguration config,
        IWebHostEnvironment env)
    {
        ConfigureCompositionRoot(serviceCollection, config);

        await PersistenceStartup.InitializeAsync(env);
        await QuartzStartup.InitializeAsync();
    }

    private static void ConfigureCompositionRoot(IServiceCollection serviceCollection, IConfiguration config)
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.Populate(serviceCollection);

        containerBuilder.RegisterModule(new PersistenceModule(config));
        containerBuilder.RegisterModule(new CQRSModule());
        containerBuilder.RegisterModule(new QuartzModule());
        containerBuilder.RegisterModule(new ProcessingModule());
        containerBuilder.RegisterModule(new AuthModule());

        var container = containerBuilder.Build();

        ChatCompositionRoot.SetContainer(container);
    }
}
