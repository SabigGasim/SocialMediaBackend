using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.Modules.Users.Application.Configuration.Mediator;
using SocialMediaBackend.Modules.Users.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Persistence;
using SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Processing;

namespace SocialMediaBackend.Modules.Users.Application.Configuration;

public static class UsersStartup
{
    public static async Task InitializeAsync(
        IServiceCollection serviceCollection, 
        IConfiguration config,
        IWebHostEnvironment env)
    {
        ConfigureCompositionRoot(serviceCollection, config);

        await PersistenceStartup.InitializeAsync(env);
    }

    private static void ConfigureCompositionRoot(IServiceCollection serviceCollection, IConfiguration config)
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.Populate(serviceCollection);

        containerBuilder.RegisterModule(new PersistenceModule(config));
        containerBuilder.RegisterModule(new CQRSModule());
        containerBuilder.RegisterModule(new ProcessingModule());

        var container = containerBuilder.Build();

        UsersCompositionRoot.SetContainer(container);
    }
}
