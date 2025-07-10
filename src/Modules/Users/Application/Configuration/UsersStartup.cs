using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SocialMediaBackend.BuildingBlocks.Application.Auth;
using SocialMediaBackend.Modules.Users.Application.Configuration.Auth;
using SocialMediaBackend.Modules.Users.Application.Configuration.Mediator;
using SocialMediaBackend.Modules.Users.Infrastructure.Configuration;
using SocialMediaBackend.Modules.Users.Infrastructure.Configuration.EventBus;
using SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Persistence;
using SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Processing;
using SocialMediaBackend.Modules.Users.Infrastructure.Configuration.Quartz;

namespace SocialMediaBackend.Modules.Users.Application.Configuration;

public static class UsersStartup
{
    public static async Task InitializeAsync(
        IServiceCollection serviceCollection, 
        string connectionString,
        IWebHostEnvironment env,
        IExecutionContextAccessor executionContextAccessor)
    {
        ConfigureCompositionRoot(serviceCollection, connectionString, executionContextAccessor);

        await PersistenceStartup.InitializeAsync(env);
        await QuartzStartup.InitializeAsync();
    }

    private static void ConfigureCompositionRoot(IServiceCollection serviceCollection, string connectionString, IExecutionContextAccessor executionContextAccessor)
    {
        var containerBuilder = new ContainerBuilder();

        containerBuilder.Populate(serviceCollection);

        containerBuilder.RegisterModule(new PersistenceModule(connectionString));
        containerBuilder.RegisterModule(new CQRSModule());
        containerBuilder.RegisterModule(new ProcessingModule());
        containerBuilder.RegisterModule(new QuartzModule());
        containerBuilder.RegisterModule(new EventBusModule());
        containerBuilder.RegisterModule(new AuthModule());

        containerBuilder.RegisterInstance(executionContextAccessor);

        var container = containerBuilder.Build();

        UsersCompositionRoot.SetContainer(container);
    }
}
