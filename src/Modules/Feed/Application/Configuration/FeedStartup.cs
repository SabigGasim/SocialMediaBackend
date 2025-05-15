using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Feed.Application.Configuration.Auth;
using SocialMediaBackend.Modules.Feed.Application.Configuration.Mediator;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Persistence;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Processing;
using SocialMediaBackend.Modules.Feed.Infrastructure.Configuration.Quartz;

namespace SocialMediaBackend.Modules.Feed.Application.Configuration;

public static class FeedStartup
{
    public static async Task InitializeAsync(
        IServiceCollection serviceCollection,
        IConfiguration config,
        IWebHostEnvironment env)
    {
        ConfigureCompositionRoot(serviceCollection, config);

        await QuartzStartup.InitializeAsync();
        await PersistenceStartup.InitializeAsync(env);
    }

    private static void ConfigureCompositionRoot(IServiceCollection serviceCollection, IConfiguration config)
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.Populate(serviceCollection);

        containerBuilder.RegisterModule(new QuartzModule());
        containerBuilder.RegisterModule(new PersistenceModule(config));
        containerBuilder.RegisterModule(new CQRSModule());
        containerBuilder.RegisterModule(new ProcessingModule());
        containerBuilder.RegisterModule(new AuthModule());

        var container = containerBuilder.Build();

        FeedCompositionRoot.SetContainer(container);
    }
}
